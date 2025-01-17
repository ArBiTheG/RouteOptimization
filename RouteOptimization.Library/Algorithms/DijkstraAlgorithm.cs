using RouteOptimization.Library.Entity;
using RouteOptimization.Library.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Library.Algorithms
{
    public class DijkstraAlgorithm: IAlgorithm
    {
        Graph _graph;

        public DijkstraAlgorithm(Graph graph)
        {
            _graph = graph;
        }

        private IEnumerable<VertexInfo> Optimize(Vertex vertexBegin)
        {
            var vertexInfoList = new HashSet<VertexInfo>();

            foreach (var vertex in _graph.Vertices)
            {
                var vertexInfo = new VertexInfo(vertex);
                if (vertexInfo.Vertex == vertexBegin)
                    vertexInfo.Weight = 0;
                vertexInfoList.Add(vertexInfo);
            }

            var notVisitedVertexInfoList = vertexInfoList.ToHashSet();

            while (notVisitedVertexInfoList.Any())
            {
                VertexInfo nearestVertexInfo = notVisitedVertexInfoList.OrderBy(v => v.Weight).First();
                notVisitedVertexInfoList.Remove(nearestVertexInfo);

                foreach (var edge in nearestVertexInfo.Vertex.Edges)
                {
                    VertexInfo neighborVertexInfo = vertexInfoList.First(v => v.Vertex == edge.To);
                    if (notVisitedVertexInfoList.Contains(neighborVertexInfo))
                    {
                        double currentWeight = nearestVertexInfo.Weight + edge.Weight;
                        if (currentWeight < neighborVertexInfo.Weight)
                        {
                            neighborVertexInfo.Weight = currentWeight;
                            neighborVertexInfo.PreviousVertex = nearestVertexInfo.Vertex;
                        }
                    }
                }
            }

            return vertexInfoList;
        }

        private double GetTotalWeight(IEnumerable<VertexInfo> vertexInfoList, Vertex vertexEnd)
        {
            VertexInfo info = vertexInfoList.First(i => i.Vertex == vertexEnd);
            return info.Weight;
        }

        private IEnumerable<Vertex> GetRoute(IEnumerable<VertexInfo> vertexInfoList, Vertex vertexBegin, Vertex vertexEnd)
        {
            Stack<Vertex> routeResult = new Stack<Vertex>();
            routeResult.Push(vertexEnd);

            Vertex? vertexCursor = vertexEnd;
            while (vertexCursor != vertexBegin)
            {
                VertexInfo info = vertexInfoList.First(i => i.Vertex == vertexCursor);
                vertexCursor = info.PreviousVertex;


                if (vertexCursor == null)
                    continue;
                //    throw new RouteException("It is impossible to build a non-existent route");

                routeResult.Push(vertexCursor);
            }
            return routeResult;
        }


        public Way BuildTo(Vertex vertexBegin, Vertex vertexEnd)
        {
            var list = Optimize(vertexBegin);

            return new Way(
                GetRoute(list, vertexBegin, vertexEnd),
                GetTotalWeight(list, vertexEnd));
        }
    }
}
