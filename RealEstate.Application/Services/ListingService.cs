using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using RealEstate.Application.Dtos;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Services
{
    public class ListingService : IListingService
    {
        private readonly RealEstateDbContext _ctx;
        private readonly IProperty _property;
        private readonly ICommissionService _commSvc;
        private readonly IMemoryCache _cache;
        //private readonly IMapper _mapper;
        public ListingService(RealEstateDbContext ctx, ICommissionService commSvc, IMemoryCache cache)
        {
            _property = property;
            //_mapper = mapper;
            _ctx = ctx;
            _commSvc = commSvc;
            _cache = cache;
        }
        public async Task<List<ListingDto>> GetAllAsync(string? location, decimal? minPrice, decimal? maxPrice, string? propertyType)
        {
            // Simple in‑memory caching of full listing list for 60s
            var cacheKey = $"listings_{location}_{minPrice}_{maxPrice}_{propertyType}";
            if (!_cache.TryGetValue(cacheKey, out List<ListingDto> list))
            {
                // query
                var query = _ctx.Listings.AsNoTracking().AsQueryable();
                if (!string.IsNullOrWhiteSpace(location))
                    query = query.Where(l => l.Location.Contains(location));
                if (minPrice.HasValue) query = query.Where(l => l.Price >= minPrice.Value);
                if (maxPrice.HasValue) query = query.Where(l => l.Price <= maxPrice.Value);
                if (!string.IsNullOrWhiteSpace(propertyType))
                    query = query.Where(l => l.PropertyType == propertyType);

                list = await query.Select(l => new ListingDto
                {
                    Id = l.Id,
                    PropertyType = l.PropertyType,
                    Location = l.Location,
                    Price = l.Price,
                    Features = l.Features,
                    Commission = l.Commission,
                    BrokerId = l.BrokerId
                }).ToListAsync();

                _cache.Set(cacheKey, list, TimeSpan.FromSeconds(60));
            }
            return list;
        }

        public async Task<ListingDto?> GetByIdAsync(Guid id)
        {
            var l = await _ctx.Listings.FindAsync(id);
            if (l == null) return null;
            return new ListingDto
            {
                Id = l.Id,
                PropertyType = l.PropertyType,
                Location = l.Location,
                Price = l.Price,
                Features = l.Features,
                Commission = l.Commission,
                BrokerId = l.BrokerId
            };
        }

        public async Task<ListingDto> CreateAsync(CreateListingDto dto, string brokerId)
        {
            var commission = await _commSvc.CalculateAsync(dto.Price);
            var entity = new Listing
            {
                Id = Guid.NewGuid(),
                PropertyType = dto.PropertyType,
                Location = dto.Location,
                Price = dto.Price,
                Features = dto.Features,
                BrokerId = brokerId,
                Commission = commission,
                CreatedAt = DateTime.UtcNow
            };
            _ctx.Listings.Add(entity);
            await _ctx.SaveChangesAsync();
            return new ListingDto
            {
                Id = entity.Id,
                PropertyType = entity.PropertyType,
                Location = entity.Location,
                Price = entity.Price,
                Features = entity.Features,
                Commission = entity.Commission,
                BrokerId = entity.BrokerId
            };
        }

        public async Task<bool> UpdateAsync(UpdateListingDto dto, string brokerId)
        {
            var l = await _ctx.Listings.FindAsync(dto.Id);
            if (l == null || l.BrokerId != brokerId) return false;

            l.PropertyType = dto.PropertyType;
            l.Location = dto.Location;
            l.Price = dto.Price;
            l.Features = dto.Features;
            l.Commission = await _commSvc.CalculateAsync(dto.Price);
            l.UpdatedAt = DateTime.UtcNow;

            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id, string brokerId)
        {
            var l = await _ctx.Listings.FindAsync(id);
            if (l == null || l.BrokerId != brokerId) return false;
            _ctx.Listings.Remove(l);
            await _ctx.SaveChangesAsync();
            return true;
        }
    }
}
