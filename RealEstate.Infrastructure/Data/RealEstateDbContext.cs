using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Infrastructure.Data
{
    public  class RealEstateDbContext: DbContext
    {
        public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options) : base(options)
        {
        }
       
        public DbSet<Listing> Listings { get; set; }
        public DbSet<CommissionRate> CommissionRates { get; set; }
    }
}
