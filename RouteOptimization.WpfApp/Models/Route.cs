using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.Models
{
    public class Route: IRoute
    {
        public int Id { get; set; }
        public int StartLocationId { get; set; }
        public Location? StartLocation { get; set; }
        public int EndLocationId { get; set; }
        public Location? EndLocation { get; set; }
        public double Distance { get; set; }
        public double Time { get; set; }
    }
}
