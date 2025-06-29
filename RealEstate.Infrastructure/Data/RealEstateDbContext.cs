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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Property>()
                .HasOne(p => p.Address)
                .WithMany()
                .HasForeignKey(p => p.AddressId);
            modelBuilder.Entity<Property>()
                .HasOne(p => p.Broker)
                .WithMany(b => b.Properties)
                .HasForeignKey(p => p.BrokerId);
            modelBuilder.Entity<Image>()
                .HasOne(i => i.Property)
                .WithMany(p => p.Images)
                .HasForeignKey(i => i.PropertyId);

            // Seed sample data
            modelBuilder.Entity<Broker>().HasData(new Broker { Id = 1, Name = "Broker One", Phone = "123-456-7890", Email = "broker1@example.com" });
            modelBuilder.Entity<Address>().HasData(new Address { Id = 1, Street = "123 Main St", City = "CityA", State = "StateA", Zip = "12345" });
            modelBuilder.Entity<Property>().HasData(new Property { Id = 1, Description = "Nice house", Price = 100000, Bedrooms = 3, Bathrooms = 2, Area = 1500, AddressId = 1, BrokerId = 1 });
            modelBuilder.Entity<Image>().HasData(new Image { Id = 1, Url = "http://example.com/image1.jpg", PropertyId = 1 });
        }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
