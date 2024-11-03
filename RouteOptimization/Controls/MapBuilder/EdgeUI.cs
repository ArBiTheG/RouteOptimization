using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Controls.MapBuilder
{
    public abstract class EdgeUI
    {
        public abstract float StartX { get; set; }
        public abstract float StartY { get; set; }
        public abstract float FinishX { get; set; }
        public abstract float FinishY { get; set; }

        public event EventHandler? Pressed;
        public event EventHandler? Released;

        public static void PerformPress(EdgeUI edge)
        {
            edge.OnPressed(EventArgs.Empty);
            edge.Pressed?.Invoke(edge, EventArgs.Empty);
        }
        public static void PerformRelease(EdgeUI edge)
        {
            edge.OnReleased(EventArgs.Empty);
            edge.Released?.Invoke(edge, EventArgs.Empty);
        }

        protected virtual void OnPressed(EventArgs e)
        {
        }

        protected virtual void OnReleased(EventArgs e)
        {
        }
    }
}
