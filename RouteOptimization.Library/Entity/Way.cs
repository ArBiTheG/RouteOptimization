using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Library.Entity
{
    public class Way
    {
        public IEnumerable<Vertex> Vertices { get; }
        public double Weight { get; }
        public Way(IEnumerable<Vertex> vertices, double weight) 
        { 
            Vertices = vertices;
            Weight = weight;
        }
    }
}
