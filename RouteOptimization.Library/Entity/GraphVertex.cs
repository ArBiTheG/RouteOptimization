using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Library.Entity
{
    public class GraphVertex
    {
        private List<GraphEdge> _edges;

        public object Name { get; }

        public GraphVertex(object name)
        {
            _edges = new List<GraphEdge>();
            Name = name;
        }

        public GraphEdge[] Edges => _edges.ToArray();

        public void AddEdge(GraphVertex vertex, float weight)
        {
            _edges.Add(new GraphEdge(vertex, weight));
        }
        public void RemoveEdge(GraphVertex vertex)
        {
            var edge = _edges.FirstOrDefault(e => e.Vertex == vertex);
            if (edge != null)
            {
                _edges.Remove(edge);
            }
        }
        public void ClearEdge()
        { 
            _edges.Clear(); 
        }

        public int CountEdges => _edges.Count;

        public override string? ToString()
        {
            return Name.ToString();
        }
    }
}
