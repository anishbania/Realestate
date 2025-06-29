using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces
{
    public interface IListingService
    {
        Task<IEnumerable<Property>> SearchPropertiesAsync(string location, decimal? minPrice, decimal? maxPrice, int? bedrooms);
        Task<PropertyDetailsDto> GetPropertyDetailsAsync(int id);
    }
}
