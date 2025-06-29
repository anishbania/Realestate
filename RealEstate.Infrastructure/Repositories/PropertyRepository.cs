using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Infrastructure.Repositories
{
    public class PropertyRepository : IProperty
    {
        private readonly RealEstateDbContext _context;
        public PropertyRepository(RealEstateDbContext context)
        {
            _context = context;
        }
        public async Task<Property> GetByIdAsync(int id)
        {
            return await _context.Properties
                       .Include(p => p.Address)
                       .Include(p => p.Broker)
                       .Include(p => p.Images)
                       .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Property>> SearchAsync(string location, decimal? minPrice, decimal? maxPrice, int? bedrooms)
        {
            var query = _context.Properties
                           .Include(p => p.Address)
                           .AsQueryable();

            if (!string.IsNullOrEmpty(location))
                query = query.Where(p => p.Address.City.Contains(location) || p.Address.State.Contains(location));
            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);
            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);
            if (bedrooms.HasValue)
                query = query.Where(p => p.Bedrooms == bedrooms.Value);

            return await query.ToListAsync();
        }
    }
}
