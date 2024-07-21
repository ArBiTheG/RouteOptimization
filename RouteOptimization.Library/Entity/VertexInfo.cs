using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Library.Entity
{
    public class VertexInfo: IComparable
    {
        public Vertex Vertex { get; }
        public double Weight { get; set; }
        public Vertex? PreviousVertex { get; set; }
        public VertexInfo(Vertex vertex)
        {
            Vertex = vertex;
            Weight = double.PositiveInfinity;
        }

        public int CompareTo(object? obj)
        {
            if (obj == null) return 1;
            VertexInfo otherVertexInfo = obj as VertexInfo;
            if (otherVertexInfo != null)
                return Weight.CompareTo(otherVertexInfo.Weight);
            else
                throw new ArgumentException("Object is not a VertexInfo");
        }
    }
}
