using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Library.Entity
{
    public class Route
    {
        public Stack<Vertex> Vertices { get; }
        public double Weight { get; }
        public Route(Stack<Vertex> vertices, double weight) 
        { 
            Vertices = vertices;
            Weight = weight;
        }
    }
}
