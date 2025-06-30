using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.Entities
{
    public class Listing
    {
        public Guid Id { get; set; }
        public string PropertyType { get; set; } = null!;
        public string Location { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Features { get; set; }
        public string BrokerId { get; set; } = null!;  // FK to ApplicationUser.Id
        public decimal Commission { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
