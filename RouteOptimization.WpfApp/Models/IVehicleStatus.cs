using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.Models
{
    public interface IVehicleStatus
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
