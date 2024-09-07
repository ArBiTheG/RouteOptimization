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
        public Vertex(double x, double y)
        {
            Size = 10;
            X = x;
            Y = y;
        }

        public bool Selected { get; set; }
        public double Size { get; set; }
        public double X { get; set; }
        public double Y { get; set; }


        private EntityPointerEventArgs pointerEventArgs = new EntityPointerEventArgs();
        private double _lastX;
        private double _lastY;

        public event EventHandler<EntityPointerEventArgs>? Moved;
        public event EventHandler? Pressed;
        public event EventHandler? Released;
        public event EventHandler? Entered;
        public event EventHandler? Exited;

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
            X = _lastX + e.X;
            Y = _lastY + e.Y;
        }

        protected virtual void OnPressed(EventArgs e)
        {
            _lastX = X;
            _lastY = Y;
            Selected = true;
        }

        protected virtual void OnReleased(EventArgs e)
        {
            Selected = false;
        }

        protected virtual void OnExited(EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnEntered(EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
