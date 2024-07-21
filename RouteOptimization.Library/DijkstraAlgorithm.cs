using RouteOptimization.Library.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Library
{
    public class DijkstraAlgorithm
    {
        private static List<VertexInfo> Run(Graph graph, Vertex source)
        {
            List<VertexInfo> vertexInfoList = new List<VertexInfo>();

            foreach (var vertex in graph.Vertices)
            {
                var vertexInfo = new VertexInfo(vertex);
                if (vertexInfo.Vertex == source)
                    vertexInfo.Weight = 0;
                vertexInfoList.Add(vertexInfo);
            }

            var notVisitedVertexInfoList = new HashSet<VertexInfo>(vertexInfoList);

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

        public static Route BuildRoute(Graph graph, Vertex vertexBegin, Vertex vertexEnd)
        {
            List<VertexInfo> vertexInfoList = Run(graph, vertexBegin);

            VertexInfo info = vertexInfoList.First(i => i.Vertex == vertexEnd);
            double weight = info.Weight;

            Stack<Vertex> routeResult = new Stack<Vertex>();
            routeResult.Push(vertexEnd);

            Vertex? vertexCursor = vertexEnd;
            while (vertexCursor != vertexBegin)
            {
                info = vertexInfoList.First(i => i.Vertex == vertexCursor);
                vertexCursor = info.PreviousVertex;

                if (vertexCursor == null)
                    throw new Exception("Нет пути в точку");

                routeResult.Push(vertexCursor);
            }

            return new Route(routeResult, weight);
        }
    }
}
