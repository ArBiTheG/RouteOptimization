using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models
{
    public class VehicleType : IVehicleType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
