using Avalonia;
using Avalonia.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Controls.MapBuilder
{
    public class Vertex: IVertex
    {
        double _lastX;
        double _lastY;

        public Vertex()
        {
            Size = 10;
        }

        public double Size { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        private EntityPointerEventArgs pointerEventArgs = new EntityPointerEventArgs();

        public event EventHandler<EntityPointerEventArgs>? Moved;
        public event EventHandler? Pressed;
        public event EventHandler? Released;

        public static void PerformMove(Vertex vertex, Point position)
        {
            vertex.pointerEventArgs.Position = position;
            vertex.OnMoved(vertex.pointerEventArgs);
            vertex.Moved?.Invoke(vertex, vertex.pointerEventArgs);
        }
        public static void PerformPress(Vertex vertex)
        {
            vertex.OnPressed(EventArgs.Empty);
            vertex.Pressed?.Invoke(vertex, EventArgs.Empty);
        }
        public static void PerformRelease(Vertex vertex)
        {
            vertex.OnReleased(EventArgs.Empty);
            vertex.Released?.Invoke(vertex, EventArgs.Empty);
        }
        public static void ClearLastPositions(Vertex vertex)
        {
            vertex._lastX = vertex.X;
            vertex._lastY = vertex.Y;
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
        }
    }
}
