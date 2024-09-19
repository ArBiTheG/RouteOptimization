using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.Models
{
    public class VehicleStatus: IVehicleStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
