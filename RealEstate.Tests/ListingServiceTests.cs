using RealEstate.Application.Services;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RealEstate.Application.Dtos;
using System;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Tests
{
    public class ListingServiceTests
    {
        private RealEstateDbContext GetContextWithListings()
        {
            var opts = new DbContextOptionsBuilder<RealEstateDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var ctx = new RealEstateDbContext(opts);
            ctx.Listings.AddRange(
                new Listing { Id = Guid.NewGuid(), Location = "CityA", Price = 100, PropertyType = "Home", BrokerId = "B1", Commission = 2 },
                new Listing { Id = Guid.NewGuid(), Location = "CityB", Price = 200, PropertyType = "Condo", BrokerId = "B2", Commission = 4 }
            );
            ctx.SaveChanges();
            return ctx;
        }

        [Fact]
        public async Task GetAllAsync_FiltersByLocation()
        {
            // Arrange
            using var ctx = GetContextWithListings();
            var commMock = new Mock<ICommissionService>();
            var cache = new MemoryCache(new MemoryCacheOptions());
            var svc = new ListingService(ctx, commMock.Object, cache);

            // Act
            var result = await svc.GetAllAsync(location: "CityA");

            // Assert
            result.Should().HaveCount(1)
                  .And.ContainSingle(x => x.Location == "CityA");
        }

        [Fact]
        public async Task CreateAsync_ComputesCommissionAndSaves()
        {
            // Arrange
            using var ctx = GetContextWithListings();
            var commMock = new Mock<ICommissionService>();
            commMock.Setup(x => x.CalculateAsync(150m)).ReturnsAsync(7.5m);
            var cache = new MemoryCache(new MemoryCacheOptions());
            var svc = new ListingService(ctx, commMock.Object, cache);

            var dto = new CreateListingDto { Location = "CityC", Price = 150, PropertyType = "Loft" };
            var brokerId = "BrokerX";

            // Act
            var created = await svc.CreateAsync(dto, brokerId);

            // Assert
            created.Location.Should().Be("CityC");
            created.Commission.Should().Be(7.5m);

            var inDb = ctx.Listings.Single(l => l.Id == created.Id);
            inDb.BrokerId.Should().Be(brokerId);
            inDb.Commission.Should().Be(7.5m);
        }

    }
}
