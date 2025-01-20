using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models.Entities
{
    public class RouteWay
    {
        public IEnumerable<Route> Routes { get; }
        public IEnumerable<Location> Locations { get; }
        public double Weight { get; }
        public RouteWay(IEnumerable<Route> routes, IEnumerable<Location> locations, double weight)
        {
            Locations = locations;
            Routes = routes;
            Weight = weight;
        }
    }
}
