using RouteOptimization.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Library.Builder
{
    public class GraphBuilder : IGraphBuilder
    {
        List<Vertex> _vertices;

        private GraphBuilder()
        {
            _vertices = new List<Vertex>();
        }

        private Vertex ReadVertex(int id)
        {
            var vertex = _vertices.FirstOrDefault(v => v.Id == id);
            if (vertex == null)
                vertex = new Vertex(id);
            return vertex;
        }

        public IGraphBuilder AddVertex(int id)
        {
            _vertices.Add(new Vertex(id));
            return this;
        }

        public IGraphBuilder AddEdge(int beginId, int endId, double weight)
        {
            var vertexBegin = ReadVertex(beginId);
            var vertexEnd = ReadVertex(endId);

            _vertices.Add(vertexBegin);
            _vertices.Add(vertexEnd);
            vertexBegin.Edges.Add(new Edge(vertexEnd, weight));
            vertexEnd.Edges.Add(new Edge(vertexBegin, weight));
            return this;
        }
        public Graph Build()
        {
            Graph graph = new Graph(_vertices);
            return graph;
        }

        public static GraphBuilder Create()
        {
            return new GraphBuilder();
        }
    }
}
