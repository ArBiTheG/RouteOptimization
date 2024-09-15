using System;
using System.Collections.Generic;
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
    }
}
