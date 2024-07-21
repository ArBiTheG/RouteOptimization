using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Library.Entity
{
    public class GraphEdge
    {
        public GraphVertex Vertex { get; }
        public float Weight { get; }

        public GraphEdge(GraphVertex vertex, float weight)
        {
            Vertex = vertex;
            Weight = weight;
        }
    }
}
