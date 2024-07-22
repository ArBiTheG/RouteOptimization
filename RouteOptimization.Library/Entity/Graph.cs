namespace RouteOptimization.Library.Entity
{
    public class Graph
    {
        public List<Vertex> Vertices { get; }
        public Graph(List<Vertex> vertices)
        {
            Vertices = vertices;
        }

        public Vertex? GetVertex(int id)
        {
            return Vertices.FirstOrDefault(x => x.Id == id);
        }
    }
}
