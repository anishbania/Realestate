using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.Entities
{
    public class CommissionRate
    {
        public int Id { get; set; }
        public decimal MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }  // null = no upper bound
        public decimal RatePercent { get; set; }  // e.g. 2.0 for 2%

    }
}
