using RealEstate.Application.Services;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using Moq;
using Xunit;

namespace RealEstate.Tests
{
    public class ListingServiceTests
    {
        [Fact]
        public async Task SearchPropertiesAsync_ShouldReturnFilteredProperties()
        {
            var mockRepo = new Mock<IProperty>();
            var properties = new List<Property> { new Property { Id = 1, Description = "Test", Price = 100000 } };
            mockRepo.Setup(repo => repo.SearchAsync("CityA", 100000, 200000, 3)).ReturnsAsync(properties);
            var service = new ListingService(mockRepo.Object);

            var result = await service.SearchPropertiesAsync("CityA", 100000, 200000, 3);

            Assert.Single(result);
            mockRepo.Verify(repo => repo.SearchAsync("CityA", 100000, 200000, 3), Times.Once());
        }

    }
}
