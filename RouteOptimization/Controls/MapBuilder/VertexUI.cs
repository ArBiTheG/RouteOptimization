using Avalonia;
using Avalonia.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Controls.MapBuilder
{
    public abstract class VertexUI
    {
        private float _lastX;
        private float _lastY;
        private EntityPointerEventArgs _pointerEventArgs = new EntityPointerEventArgs();

        public abstract float X { get; set; }
        public abstract float Y { get; set; }


        public event EventHandler<EntityPointerEventArgs>? Moved;
        public event EventHandler? Pressed;
        public event EventHandler? Released;

        public static void PerformMove(VertexUI vertex, Point position)
        {
            vertex._pointerEventArgs.Position = position;
            vertex.OnMoved(vertex._pointerEventArgs);
            vertex.Moved?.Invoke(vertex, vertex._pointerEventArgs);
        }
        public static void PerformPress(VertexUI vertex)
        {
            vertex.OnPressed(EventArgs.Empty);
            vertex.Pressed?.Invoke(vertex, EventArgs.Empty);
        }
        public static void PerformRelease(VertexUI vertex)
        {
            vertex.OnReleased(EventArgs.Empty);
            vertex.Released?.Invoke(vertex, EventArgs.Empty);
        }
        public static void ClearLastPositions(VertexUI vertex)
        {
            vertex._lastX = vertex.X;
            vertex._lastY = vertex.Y;
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
        }
    }
}
