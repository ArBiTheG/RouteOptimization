using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RouteOptimization.Library.Entity
{
    public class VertexInfo: IComparable<VertexInfo>
    {
        public Vertex Vertex { get; }
        public double Weight { get; set; }
        public Vertex? PreviousVertex { get; set; }
        public VertexInfo(Vertex vertex)
        {
            Vertex = vertex;
            Weight = double.PositiveInfinity;
        }

        public int CompareTo(VertexInfo? vertex)
        {
            if (vertex is null) throw new ArgumentException("Некорректное значение параметра");
            return (int)(this.Weight - vertex.Weight);
        }
    }
}
