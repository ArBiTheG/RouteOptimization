using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Library.Entity
{
    public class Graph
    {
        private List<GraphVertex> _vertices;
        public Graph()
        {
            _vertices = new List<GraphVertex>();
        }
        public void AddVertex(object name)
        {
            _vertices.Add(new GraphVertex(name));
        }
        public void RemoveVertex(object name)
        {
            _vertices.Remove(GetVertexByName(name));
        }

        public GraphVertex[] Vertices => _vertices.ToArray();

        public GraphVertex GetVertexByName(object vertexName)
        {
            foreach (var v in _vertices)
            {
                if (v.Name.Equals(vertexName))
                {
                    return v;
                }
            }
            throw new NullReferenceException();
        }

        public void AddEdge(object firstName, object secondName, int weight)
        {
            var firstVertex = GetVertexByName(firstName);
            var secondVertex = GetVertexByName(secondName);
            if (secondVertex != null && firstVertex != null)
            {
                firstVertex.AddEdge(secondVertex, weight);
                secondVertex.AddEdge(firstVertex, weight);
            }
        }
        public void RemoveEdge(object firstName, object secondName, int weight)
        {
            var firstVertex = GetVertexByName(firstName);
            var secondVertex = GetVertexByName(secondName);
            if (secondVertex != null && firstVertex != null)
            {
                firstVertex.RemoveEdge(secondVertex);
                secondVertex.RemoveEdge(firstVertex);
            }
        }

    }
}
