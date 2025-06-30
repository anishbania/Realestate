using Microsoft.AspNetCore.Mvc;
using Moq;
using RealEstate.Application.Dtos;
using RealEstate.Domain.Interfaces;
using RealState.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RealEstate.Tests
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesControllerTests :Controller
    {
        [Fact]
        public async Task Search_ShouldReturnOkWithProperties()
        {
            var mockService = new Mock<IListingService>();
            var properties = new List<PropertyDto> { new PropertyDto { Id = 1, Description = "Test" } };
            mockService.Setup(service => service.SearchPropertiesAsync(It.IsAny<string>(), It.IsAny<decimal?>(), It.IsAny<decimal?>(), It.IsAny<int?>())).ReturnsAsync(properties);
            var controller = new PropertiesController(mockService.Object);

            var result = await controller.Search("CityA", 100000, 200000, 3);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProperties = Assert.IsAssignableFrom<IEnumerable<PropertyDto>>(okResult.Value);
            Assert.Single(returnedProperties);
        }
    }
}
