using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Domain.Interfaces;

namespace RealState.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IListingService _listingService;

        public PropertiesController(IListingService listingService)
        {
            _listingService = listingService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string location, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice, [FromQuery] int? bedrooms)
        {
            var properties = await _listingService.SearchPropertiesAsync(location, minPrice, maxPrice, bedrooms);
            return Ok(properties);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var property = await _listingService.GetPropertyDetailsAsync(id);
            if (property == null) return NotFound();
            return Ok(property);
        }

    }
}
