using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Controls.MapBuilder
{
    public interface IEdge
    {
        double StartX { get; set; }
        double StartY { get; set; }
        double FinishX { get; set; }
        double FinishY { get; set; }
    }
}
