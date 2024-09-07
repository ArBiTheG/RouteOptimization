using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Controls.MapBuilder
{
    public class Edge: IEdge
    {
        public IVertex VertexFrom { get; set; }
        public IVertex VertexTo { get; set; }
        public bool Selected { get; set; }

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
