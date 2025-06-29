using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RealEstate.Domain.Entities
{
    public class Property
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public decimal Area { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public int BrokerId { get; set; }
        public Broker Broker { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
