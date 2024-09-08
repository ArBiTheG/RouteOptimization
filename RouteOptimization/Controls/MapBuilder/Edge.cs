using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Controls.MapBuilder
{
    public class Edge: IEdge
    {
        bool _selected;
        bool _focused;

        public IVertex VertexFrom { get; set; }
        public IVertex VertexTo { get; set; }
        public bool Selected { get => _selected; }
        public bool Focused { get => _focused; }

        public Edge(IVertex vertexTo, IVertex vertexFrom)
        {
            VertexTo = vertexTo;
            VertexFrom = vertexFrom;
        }

        public event EventHandler? Pressed;
        public event EventHandler? Released;
        public event EventHandler? Entered;
        public event EventHandler? Exited;

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

        protected virtual void OnPressed(EventArgs e)
        {
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
