namespace RouteOptimization.Library.Entity
{
    public class Graph
    {
        public HashSet<Vertex> Vertices { get; }
        public Graph(HashSet<Vertex> vertices)
        {
            Vertices = vertices;
        }

        public Vertex? GetVertex(int id)
        {
            return Vertices.FirstOrDefault(x => x.Id == id);
        }
    }
}
