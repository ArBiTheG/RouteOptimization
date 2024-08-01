using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Controls.MapBuilder
{
    public class Edge
    {
        public Vertex VertexFrom { get; set; }
        public Vertex VertexTo { get; set; }
        public bool Selected;
        public Edge(Vertex vertexTo, Vertex vertexFrom)
        {
            VertexTo = vertexTo;
            VertexFrom = vertexFrom;
        }
    }
}
