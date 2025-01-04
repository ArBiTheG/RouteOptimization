using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models.Entities
{
    public interface IShipment
    {
        int Id { get; }
        string Name { get; set; }
        double Weight { get; set; }
        DateTime DateTime { get; set; }
        Location? Origin { get; set; }
        Location? Destination { get; set; }
    }
}
