using Avalonia;
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
        const double DEFAULT_GRID_SIZE = 50;

        private double _x;
        private double _y;
        private double _zoom;
        private double _gridSize;

        private int _zoomIndex = 3;
        private double[] _zooms         = { 0.125, 0.25, 0.5, 1.0, 2.0, 4.0,  8.0 };

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
            set => this.RaiseAndSetIfChanged(ref _zoom, value);
        }
        public double GridSize
        {
            get => _gridSize;
            set => this.RaiseAndSetIfChanged(ref _gridSize, value);
        }
        public Scene(double x, double y)
        {
            X = x;
            Y = y;
            Zoom        = _zooms[_zoomIndex];
            GridSize    = DEFAULT_GRID_SIZE;
        }

        private EntityPointerEventArgs pointerEventArgs = new EntityPointerEventArgs();
        private EntityPointerWheelEventArgs pointerWheelEventArgs = new EntityPointerWheelEventArgs();
        private double _lastX;
        private double _lastY;

        public event EventHandler<EntityPointerEventArgs>? Moved;
        public event EventHandler? Pressed;
        public event EventHandler? Released;
        public event EventHandler<EntityPointerWheelEventArgs>? WheelChanged;

        public static void PerformMove(Scene scene, Point position)
        {
            scene.pointerEventArgs.Position = position;

            scene.OnMoved(scene.pointerEventArgs);
            scene.Moved?.Invoke(scene, scene.pointerEventArgs);
        }
        public static void PerformPress(Scene scene)
        {
            scene.OnPressed(EventArgs.Empty);
            scene.Pressed?.Invoke(scene, EventArgs.Empty);
        }
        public static void PerformRelease(Scene scene)
        {
            scene.OnReleased(EventArgs.Empty);
            scene.Released?.Invoke(scene, EventArgs.Empty);
        }
        public static void PerformWheelChanged(Scene scene, Vector delta)
        {
            scene.pointerWheelEventArgs.Delta = delta;

            scene.OnWheelChanged(scene.pointerWheelEventArgs);
            scene.WheelChanged?.Invoke(scene, scene.pointerWheelEventArgs);
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
            if (e.Delta.Y > 0)
            {
                if (_zoomIndex > 0)
                    _zoomIndex--;
            }
            else
            {
                if (_zoomIndex < _zooms.Length - 1)
                    _zoomIndex++;
            }
            Zoom = _zooms[_zoomIndex];
        }

    }
}
