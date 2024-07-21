
using RouteOptimization.Library;
using RouteOptimization.Library.Entity;

Graph graph = new Graph();

Vertex vertexA = new Vertex("A");
Vertex vertexB = new Vertex("B");
Vertex vertexC = new Vertex("C");
Vertex vertexD = new Vertex("D");

graph.Vertices.AddRange(new[] { vertexA, vertexB, vertexC, vertexD });

vertexA.Edges.Add(new Edge(vertexA, vertexB, 1));

vertexB.Edges.Add(new Edge(vertexB, vertexC, 2));

vertexC.Edges.Add(new Edge(vertexC, vertexB, 2));
vertexB.Edges.Add(new Edge(vertexB, vertexD, 5));
vertexD.Edges.Add(new Edge(vertexD, vertexA, 5));

Route route = DijkstraAlgorithm.BuildRoute(graph, vertexC, vertexA);

Console.WriteLine(route.Weight);

foreach (var shortestPath in route.Vertices)
{
    Console.WriteLine(shortestPath.Id);
}
