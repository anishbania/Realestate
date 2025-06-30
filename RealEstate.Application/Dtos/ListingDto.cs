using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Dtos
{
    public class ListingDto
    {
        public Guid Id { get; set; }
        public string PropertyType { get; set; } = "";
        public string Location { get; set; } = "";
        public decimal Price { get; set; }
        public string? Features { get; set; }
        public decimal Commission { get; set; }
        public string BrokerId { get; set; } = "";
    }
}
