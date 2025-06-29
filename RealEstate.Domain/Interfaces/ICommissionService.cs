using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.Interfaces
{
    public interface ICommissionService
    {
        decimal CalculateCommission(decimal propertyPrice);
    }
}
