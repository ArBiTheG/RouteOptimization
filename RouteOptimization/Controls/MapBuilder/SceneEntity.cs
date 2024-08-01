using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Controls.MapBuilder
{
    public class SceneEntity
    {
        private double _zoom;

        public double X { get; set; }
        public double Y { get; set; }
        public double Zoom
        {
            get => _zoom;
            set => _zoom = ValidateZoomValue(value);
        }
        public SceneEntity(double x, double y, double zoom = 1)
        {
            X = x;
            Y = y;
            Zoom = ValidateZoomValue(zoom);
        }


        private double ValidateZoomValue(double value)
        {
            if (value < 0.01)
                return 0.01;
            if (value > 100)
                return 100;
            return value;
        }

    }
}
