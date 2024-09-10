using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models
{
    public class Shipment : IShipment
    {
        public int Id { get; set; }
        public double Weight { get; set; }
        public DateTime DateTime { get; set; }
        public ILocation? Origin { get; set; }
        public ILocation? Destination { get; set; }
    }
}
