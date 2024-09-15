using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.Models
{
    public interface IShipment
    {
        int Id { get; set; }
        double Weight { get; set; }
        DateTime DateTime { get; set; }
        Location? Origin { get; set; }
        Location? Destination { get; set; }
    }
}
