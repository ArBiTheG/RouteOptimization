using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.Models
{
    public interface IVehicleType
    {
        int Id { get; }
        string? Name { get; set; }
    }
}
