using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Controls.MapBuilder
{
    public class Vertex
    {
        public Vertex(double x, double y)
        {

            X = x;
            Y = y;
            LastX = x;
            LastY = y;
        }
        public bool Selected;
        public readonly double Size = 10;
        public double X { get; set; }
        public double Y { get; set; }
        public double LastX;
        public double LastY;
    }
}
