using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.Models
{
    public interface IVehicle
    {
        int Id { get; }
        VehicleType? Type { get; set; }
        double Capacity { get; set; }
        string LicensePlate { get; set; }
        VehicleStatus? Status { get; set; }
    }
}
