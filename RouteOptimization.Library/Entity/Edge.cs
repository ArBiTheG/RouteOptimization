using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Library.Entity
{
    public class Edge
    {
        public Vertex To { get; }
        public double Weight { get; }

        public Edge(Vertex to, double weight)
        {
            To = to;
            Weight = Math.Abs(weight);
        }
    }
}
