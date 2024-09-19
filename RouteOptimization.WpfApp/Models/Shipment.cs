using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.Models
{
    public class Shipment: IShipment
    {
        public int Id { get; set; }
        public double Weight { get; set; }
        public DateTime DateTime { get; set; }
        public int OriginId { get; set; }
        public Location? Origin { get; set; }
        public int DestinationId { get; set; }
        public Location? Destination { get; set; }
    }
}
