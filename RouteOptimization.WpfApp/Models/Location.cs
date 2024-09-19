using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.Models
{
    public class Location: ILocation
    {
        public Location()
        {

        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        [NotMapped]
        public List<Route> Routes { get; set; } = new List<Route>();

        [NotMapped]
        public List<Shipment> Shipments { get; set; } = new List<Shipment>();
    }
}
