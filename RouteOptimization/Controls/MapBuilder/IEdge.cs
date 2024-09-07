using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Controls.MapBuilder
{
    public interface IEdge
    {
        IVertex VertexFrom { get; set; }
        IVertex VertexTo { get; set; }
        bool Selected { get; }
    }
}
