using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Controls.MapBuilder
{
    public interface IEdge
    {
        float StartX { get; set; }
        float StartY { get; set; }
        float FinishX { get; set; }
        float FinishY { get; set; }
    }
}
