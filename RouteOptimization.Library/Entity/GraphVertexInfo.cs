using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Library.Entity
{
    public class GraphVertexInfo
    {
        /// <summary>
        /// Вершина
        /// </summary>
        public GraphVertex Vertex { get; set; }

        /// <summary>
        /// Посещенная вершина
        /// </summary>
        public bool Visited { get; set; }

        /// <summary>
        /// Сумма веса
        /// </summary>
        public float Weight { get; set; }

        /// <summary>
        /// Предыдущая вершина
        /// </summary>
        public GraphVertex? PreviousVertex { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="vertex">Вершина</param>
        public GraphVertexInfo(GraphVertex vertex)
        {
            Vertex = vertex;
            Visited = false;
            Weight = int.MaxValue;
            PreviousVertex = null;
        }
    }
}
