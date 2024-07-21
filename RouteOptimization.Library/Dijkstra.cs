using RouteOptimization.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Library
{
    public class Dijkstra
    {
        Graph _graph;

        List<GraphVertexInfo> _vertexInfoList;

        public Dijkstra(Graph graph)
        {
            _graph = graph;
            _vertexInfoList = new List<GraphVertexInfo>();
            foreach (var v in graph.Vertices)
            {
                _vertexInfoList.Add(new GraphVertexInfo(v));
            }
        }

        public string FindShortestPath(string startName, string finishName)
        {
            return FindShortestPath(_graph.GetVertexByName(startName), _graph.GetVertexByName(finishName));
        }

        public string FindShortestPath(GraphVertex startVertex, GraphVertex finishVertex)
        {
            GraphVertexInfo? firstVertexInfo = GetVertexInfo(startVertex);
            if (firstVertexInfo!=null) firstVertexInfo.Weight = 0;
        
            while (true)
            {
                GraphVertexInfo? currentVertexInfo = FindUnvisitedVertexWithMinWeight();
                if (currentVertexInfo != null)
                    SetWeightToNextVertex(currentVertexInfo);
                else
                    break;
            }
        
            return GetPath(startVertex, finishVertex);
        }

        /// <summary>
        /// Получить непосещённую вершину с минимальным весом
        /// </summary>
        /// <returns>Сведенья о вершине</returns>
        private GraphVertexInfo? FindUnvisitedVertexWithMinWeight()
        {
            GraphVertexInfo? result = null;
            var minValue = float.MaxValue;

            foreach (var vertexInfo in _vertexInfoList)
            {
                if (vertexInfo.Visited == false &&
                    vertexInfo.Weight < minValue)
                {
                    result = vertexInfo;
                    minValue = vertexInfo.Weight;
                }
            }

            return result;
        }

        void SetWeightToNextVertex(GraphVertexInfo info)
        {
            foreach (GraphEdge edge in info.Vertex.Edges)
            {
                GraphVertexInfo? nextVertexInfo = GetVertexInfo(edge.Vertex);
                if (nextVertexInfo != null)
                {
                    float sum = info.Weight + edge.Weight;
                    if (sum < nextVertexInfo.Weight)
                    {
                        nextVertexInfo.Weight = sum;
                        nextVertexInfo.PreviousVertex = info.Vertex;
                    }
                }
            }
            info.Visited = true;
        }

        private GraphVertexInfo? GetVertexInfo(GraphVertex vertex)
        {
            return _vertexInfoList.FirstOrDefault(infos => infos.Vertex == vertex);
        }

        private string GetPath(GraphVertex vertexInfoBegin, GraphVertex vertexInfoEnd)
        {
            string? path = vertexInfoEnd.ToString();
            while (vertexInfoBegin != vertexInfoEnd)
            {
                vertexInfoEnd = GetVertexInfo(vertexInfoEnd).PreviousVertex;

                path = vertexInfoEnd.ToString() + path;
            }

            return path;
        }
    }
}
