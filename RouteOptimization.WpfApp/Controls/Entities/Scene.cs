using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RouteOptimization.WpfApp.Controls.Entities
{
    public class Scene
    {
        private double _x;
        private double _y;
        private double _zoom;

        public double X { 
            get => _x; 
            set => _x = value; 
        }
        public double Y { 
            get => _y;
            set => _y = value;
        }
        public double Zoom
        {
            get => _zoom;
            set => _zoom = ValidateZoomValue(value);
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
        private EntityPointerWheelEventArgs pointerWheelEventArgs = new EntityPointerWheelEventArgs();
        private double _lastX;
        private double _lastY;

        public event EventHandler<EntityPointerEventArgs>? Moved;
        public event EventHandler? Pressed;
        public event EventHandler? Released;
        public event EventHandler<EntityPointerWheelEventArgs>? WheelChanged;

        public void PerformMove(Point position)
        {
            pointerEventArgs.Position = position;

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
        public void PerformWheelChanged(int delta)
        {
            pointerWheelEventArgs.Delta = delta;

            OnWheelChanged(pointerWheelEventArgs);
            WheelChanged?.Invoke(this, pointerWheelEventArgs);
        }
        protected virtual void OnMoved(EntityPointerEventArgs e)
        {
            X = _lastX + e.Position.X;
            Y = _lastY + e.Position.Y;
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

        protected virtual void OnWheelChanged(EntityPointerWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (Zoom <= 100)
                    Zoom *= 1.10;
            }
            else
            {
                if (Zoom >= 0.05)
                    Zoom /= 1.10;
            }
        }

    }
}
