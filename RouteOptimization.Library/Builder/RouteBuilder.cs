using RouteOptimization.Library.Algorithms;
using RouteOptimization.Library.Entity;
using RouteOptimization.Library.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Library.Builder
{
    public class RouteBuilder : IRouteBuilder
    {
        IAlgorithm _algorithm;
        Graph _graph;

        int _beginId;
        int _endId;


        private RouteBuilder(Graph graph)
        {
            _algorithm = new DijkstraAlgorithm(graph);
            _graph = graph;
        }

        public IRouteBuilder SetBegin(int id)
        {

            _beginId = id;
            return this;
        }

        public IRouteBuilder SetEnd(int id)
        {
            _endId = id;
            return this;
        }

        public Route Build()
        {
            var vertexBegin = _graph.GetVertex(_beginId);
            var vertexEnd = _graph.GetVertex(_endId);

            if (vertexBegin == null)
                throw new GraphVertexException("The specified ID of the initial vertex is missing from the graph");
            if (vertexEnd == null)
                throw new GraphVertexException("The specified ID of the end vertex is missing from the graph");


            return _algorithm.BuildTo(vertexBegin, vertexEnd);
        }

        public static RouteBuilder Create(Graph graph)
        {
            return new RouteBuilder(graph);
        }
    }
}
