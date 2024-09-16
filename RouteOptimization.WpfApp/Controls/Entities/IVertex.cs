using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.Controls.Entities
{
    public interface IVertex
    {
        double X { get; set; }
        double Y { get; set; }
        double Size { get; set; }
        bool Selected { get; }
        bool Focused { get; }
    }
}
