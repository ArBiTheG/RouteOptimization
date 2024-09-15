using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.Models
{
    public class Vehicle: IVehicle
    {
        public int Id { get; set; }
        public VehicleType? Type { get; set; }
        public double Capacity { get; set; }
        public string LicensePlate { get; set; }
        public VehicleStatus? Status { get; set; }
    }
}
