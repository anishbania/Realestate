using AutoMapper.Configuration;
using RealEstate.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Services
{
    public class CommissionService : ICommissionService
    {
        private const decimal CommissionRate = 0.05m; // 5% commission rate
        public decimal CalculateCommission(decimal propertyPrice)
        {
            return propertyPrice * CommissionRate;
        }
    }
}
