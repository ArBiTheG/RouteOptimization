using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models
{
    public interface IVehicle
    {
        int Id { get; set; }
        IVehicleType? Type { get; set; }
        double Capacity { get; set; }
        string LicensePlate { get; set; }
        IVehicleStatus? Status { get; set; }
    }
}
