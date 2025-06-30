using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Dtos
{
    public class CreateListingDto
    {
        [Required] public string PropertyType { get; set; } = "";
        [Required] public string Location { get; set; } = "";
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        public string? Features { get; set; }
    }
}
