using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Controls.MapBuilder
{
    public class Scene: ReactiveObject, IScene
    {
        private double _x;
        private double _y;
        private double _zoom;

        public double X { 
            get => _x; 
            set => this.RaiseAndSetIfChanged(ref _x, value); 
        }
        public double Y { 
            get => _y;
            set => this.RaiseAndSetIfChanged(ref _y, value);
        }
        public double Zoom
        {
            get => _zoom;
            set => this.RaiseAndSetIfChanged(ref _zoom, ValidateZoomValue(value));
        }
        public Scene(double x, double y, double zoom = 1)
        {
            X = x;
            Y = y;
            Zoom = zoom;
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
