using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;
using System.Security.Claims;

namespace RealState.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]                  // only authenticated users
    public class ListingController : ControllerBase
    {
        private readonly IListingService _svc;
        private readonly ApiResponse _response;
        public ListingController(IListingService svc)
        {
            _svc = svc;
            _response = new ApiResponse();
        }
        // GET: api/Listing
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? location, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice, [FromQuery] string? propertyType)
        {
            var list = await _svc.GetAllAsync(location, minPrice, maxPrice, propertyType);
            _response.Result = list;
            _response.IsSuccess = true;
            return Ok(_response);
        }

        // GET: api/Listing/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _svc.GetByIdAsync(id);
            if (item == null)
                return NotFound(new ApiResponse { IsSuccess = false, ErrorMessages = new[] { "Not found" } });
            _response.Result = item;
            _response.IsSuccess = true;
            return Ok(_response);
        }

        // POST: api/Listing
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateListingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var brokerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var created = await _svc.CreateAsync(dto, brokerId);
            _response.Result = created;
            _response.IsSuccess = true;
            return CreatedAtAction(nameof(GetById),
                new { id = created.Id }, _response);
        }

        // PUT: api/Listing
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateListingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var brokerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var ok = await _svc.UpdateAsync(dto, brokerId);
            if (!ok)
                return Forbid();

            _response.IsSuccess = true;
            return NoContent();
        }

        // DELETE: api/Listing/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var brokerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var ok = await _svc.DeleteAsync(id, brokerId);
            if (!ok)
                return Forbid();

            _response.IsSuccess = true;
            return NoContent();
        }
    }
}
