using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models
{
    public interface IVehicleStatus
    {
        int Id { get; }
        string Name { get; set; }
    }
}
