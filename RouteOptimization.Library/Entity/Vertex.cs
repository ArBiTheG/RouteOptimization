using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Library.Entity
{
    public class Vertex
    {
        public object Id { get; }
        public List<Edge> Edges { get; }

        public Vertex(object id)
        {
            Id = id;
            Edges = new List<Edge>();
        }
    }
}
