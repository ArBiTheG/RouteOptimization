using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Controls.MapBuilder
{
    public struct ScenePoint: IScenePoint
    {
        public float X { get; set; }
        public float Y { get; set; }
        public ScenePoint(float x = 0, float y = 0)
        {
            X = x;
            Y = y;
        }
        public ScenePoint(double x = 0, double y = 0)
        {
            X = (float)x;
            Y = (float)y;
        }
    }
}
