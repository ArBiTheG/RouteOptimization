using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Controls.MapBuilder
{
    public interface IScene
    {
        double X { get; set; }
        double Y { get; set; }
        double Zoom { get; set; }
    }
}
