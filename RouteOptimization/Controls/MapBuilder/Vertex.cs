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
        bool _selected;
        bool _focused;
        double _lastX;
        double _lastY;

        public Vertex()
        {
            Size = 10;
        }

        public bool Selected { get => _selected; }
        public bool Focused { get => _focused; }
        public double Size { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        private EntityPointerEventArgs pointerEventArgs = new EntityPointerEventArgs();

        public event EventHandler<EntityPointerEventArgs>? Moved;
        public event EventHandler? Pressed;
        public event EventHandler? Released;
        public event EventHandler? Entered;
        public event EventHandler? Exited;

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
        public void PerformEnter()
        {
            OnEntered(EventArgs.Empty);
            Entered?.Invoke(this, EventArgs.Empty);
        }
        public void PerformExit()
        {
            OnExited(EventArgs.Empty);
            Exited?.Invoke(this, EventArgs.Empty);
        }
        public void ClearLastPositions()
        {
            _lastX = X;
            _lastY = Y;
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
            _selected = true;
        }

        protected virtual void OnReleased(EventArgs e)
        {
            _selected = false;
        }

        protected virtual void OnEntered(EventArgs e)
        {
            _focused = true;
        }

        protected virtual void OnExited(EventArgs e)
        {
            _focused = false;
        }
    }
}
