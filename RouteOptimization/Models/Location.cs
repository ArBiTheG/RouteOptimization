using RouteOptimization.Controls.MapBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models
{
    public class Location : Vertex, ILocation
    {
        public Location()
        {

        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
