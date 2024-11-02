using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Controls.MapBuilder
{
    public class Edge: IEdge
    {
        public IVertex VertexFrom { get; set; }
        public IVertex VertexTo { get; set; }

        public Edge(IVertex vertexTo, IVertex vertexFrom)
        {
            VertexTo = vertexTo;
            VertexFrom = vertexFrom;
        }

        public event EventHandler? Pressed;
        public event EventHandler? Released;

        public static void PerformPress(Edge edge)
        {
            edge.OnPressed(EventArgs.Empty);
            edge.Pressed?.Invoke(edge, EventArgs.Empty);
        }
        public static void PerformRelease(Edge edge)
        {
            edge.OnReleased(EventArgs.Empty);
            edge.Released?.Invoke(edge, EventArgs.Empty);
        }

        protected virtual void OnPressed(EventArgs e)
        {
        }

        protected virtual void OnReleased(EventArgs e)
        {
        }
    }
}
