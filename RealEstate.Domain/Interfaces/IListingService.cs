using RealEstate.Application.Dtos;

namespace RealEstate.Domain.Interfaces
{
    public interface IListingService
    {
        Task<List<ListingDto>> GetAllAsync(string? location = null, decimal? minPrice = null, decimal? maxPrice = null, string? propertyType = null);
        Task<ListingDto?> GetByIdAsync(Guid id);
        Task<ListingDto> CreateAsync(CreateListingDto dto, string brokerId);
        Task<bool> UpdateAsync(UpdateListingDto dto, string brokerId); 
        Task<bool> DeleteAsync(Guid id, string brokerId);
    }
}
