using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;
namespace RealEstate.Application.Services
{
    public class CommissionService : ICommissionService
    {
        private readonly RealEstateDbContext _ctx;
        private const decimal CommissionRate = 0.05m; // 5% commission rate
        public async Task<decimal> CalculateAsync(decimal price)
        {
            var rate = await _ctx.CommissionRates
                .OrderBy(r => r.MinPrice)
                .FirstOrDefaultAsync(r => price >= r.MinPrice
                    && (r.MaxPrice == null || price <= r.MaxPrice.Value));

            if (rate == null) throw new InvalidOperationException("No commission rate configured.");

            return Math.Round(price * rate.RatePercent / 100m, 2);
        }
    }
}
