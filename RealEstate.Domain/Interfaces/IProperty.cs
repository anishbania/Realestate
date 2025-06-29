using RealEstate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.Interfaces
{
    public interface IProperty
    {
        Task<IEnumerable<Property>> SearchAsync(string location, decimal? minPrice, decimal? maxPrice, int? bedrooms);
        Task<Property> GetByIdAsync(int id);
    }
}
