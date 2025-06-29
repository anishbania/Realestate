using AutoMapper;
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
        private readonly IProperty _property;
        private readonly IMapper _mapper;
        public ListingService(IProperty property, IMapper mapper)
        {
            _property = property;
            _mapper = mapper;
        }
        public async Task<PropertyDetailsDto> GetPropertyDetailsAsync(int id)
        {
            var property = await _property.GetByIdAsync(id);
            return _mapper.Map<PropertyDetailsDto>(property);
        }

        public async Task<IEnumerable<Property>> SearchPropertiesAsync(string location, decimal? minPrice, decimal? maxPrice, int? bedrooms)
        {
            var properties = await _property.SearchAsync(location, minPrice, maxPrice, bedrooms);
            return (IEnumerable<Property>)_mapper.Map<IEnumerable<PropertyDto>>(properties);
        }
    }
}
