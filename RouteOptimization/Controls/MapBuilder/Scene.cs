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
        const float DEFAULT_GRID_SIZE = 50;

        private float _x;
        private float _y;

        private float _lastX;
        private float _lastY;

        private float _zoom;
        private float _gridSize;

        private int _zoomIndex = 9;
        private float[] _zooms = { 0.10f, 0.20f, 0.30f, 0.40f, 0.50f, 0.60f, 0.70f, 0.80f, 0.90f, 1.0f, 
            1.1f, 1.2f, 1.3f, 1.4f, 1.5f, 1.75f, 2.0f, 2.5f, 3.0f, 4.0f, 5.0f};

        public float X { 
            get => _x; 
            set => this.RaiseAndSetIfChanged(ref _x, value); 
        }
        public float Y { 
            get => _y;
            set => this.RaiseAndSetIfChanged(ref _y, value);
        }
        public float Zoom
        {
            get => _zoom;
            set => this.RaiseAndSetIfChanged(ref _zoom, value);
        }
        public float GridSize
        {
            get => _gridSize;
            set => this.RaiseAndSetIfChanged(ref _gridSize, value);
        }
        public Scene(float x, float y)
        {
            X = x;
            Y = y;
            Zoom        = _zooms[_zoomIndex];
            GridSize    = DEFAULT_GRID_SIZE;
        }

        private EntityPointerEventArgs pointerEventArgs = new EntityPointerEventArgs();
        private EntityPointerWheelEventArgs pointerWheelEventArgs = new EntityPointerWheelEventArgs();

        public event EventHandler<EntityPointerEventArgs>? Moved;
        public event EventHandler? Pressed;
        public event EventHandler? Released;
        public event EventHandler<EntityPointerWheelEventArgs>? WheelChanged;

        public static void PerformMove(Scene scene, ScenePoint position)
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
            X = _lastX + (float)e.Position.X;
            Y = _lastY + (float)e.Position.Y;
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
            if (e.Delta.Y < 0)
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
