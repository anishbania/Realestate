using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Services;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Data;
using Xunit;

namespace RealEstate.Tests
{
    public class CommissionServiceTests
    {
        private RealEstateDbContext GetInMemoryContext()
        {
            var opts = new DbContextOptionsBuilder<RealEstateDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var ctx = new RealEstateDbContext(opts);
            ctx.CommissionRates.AddRange(
                new CommissionRate { MinPrice = 0, MaxPrice = 99, RatePercent = 1m },
                new CommissionRate { MinPrice = 100, MaxPrice = 499, RatePercent = 2m },
                new CommissionRate { MinPrice = 500, MaxPrice = null, RatePercent = 3m }
            );
            ctx.SaveChanges();
            return ctx;
        }

        [Theory]
        [InlineData(50, 0.50)]
        [InlineData(250, 5.00)]
        [InlineData(750, 22.50)]
        public async Task CalculateAsync_Returns_CorrectCommission(decimal price, decimal expected)
        {
            // Arrange
            using var ctx = GetInMemoryContext();
            var svc = new CommissionService(ctx);

            // Act
            var commission = await svc.CalculateAsync(price);

            // Assert
            commission.Should().Be(expected);
        }

        [Fact]
        public async Task CalculateAsync_Throws_WhenNoRate()
        {
            using var ctx = GetInMemoryContext();
            ctx.CommissionRates.RemoveRange(ctx.CommissionRates);
            ctx.SaveChanges();

            var svc = new CommissionService(ctx);
            await Assert.ThrowsAsync<InvalidOperationException>(() => svc.CalculateAsync(100m));
        }
    }
}
