using Avalonia.Input;
using Avalonia.Styling;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Controls.MapBuilder
{
    public class Scene: ReactiveObject
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

        private EntityPointerEventArgs pointerEventArgs = new EntityPointerEventArgs();
        private double _lastX;
        private double _lastY;

        public event EventHandler<EntityPointerEventArgs>? Moved;
        public event EventHandler? Pressed;
        public event EventHandler? Released;

        public void PerformMove(double x, double y)
        {
            pointerEventArgs.X = x;
            pointerEventArgs.Y = y;

            OnMoved(pointerEventArgs);
            Moved?.Invoke(this, pointerEventArgs);
        }
        public void PerformPress()
        {
            OnPressed(EventArgs.Empty);
            Pressed?.Invoke(this, EventArgs.Empty);
        }
        public void PerformRelease()
        {
            OnReleased(EventArgs.Empty);
            Released?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnMoved(EntityPointerEventArgs e)
        {
            X = _lastX + e.X;
            Y = _lastY + e.Y;
        }

        protected virtual void OnPressed(EventArgs e)
        {
            _lastX = X;
            _lastY = Y;
        }

        protected virtual void OnReleased(EventArgs e)
        {
            _lastX = X;
            _lastY = Y;
        }

    }
}
