using RealEstate.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RealEstate.Tests
{
    public class CommissionServiceTests
    {
        [Fact]
        public void CalculateCommission_ShouldReturnCorrectCommission()
        {
            var service = new CommissionService();
            decimal propertyPrice = 100000;
            decimal expectedCommission = 5000; // 5%
            var result = service.CalculateCommission(propertyPrice);
            Assert.Equal(expectedCommission, result);
        }
    }
}
