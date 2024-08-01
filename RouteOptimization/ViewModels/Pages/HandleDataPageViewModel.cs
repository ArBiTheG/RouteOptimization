using RouteOptimization.Controls.MapBuilder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages
{
    public class HandleDataPageViewModel: ViewModelBase
    {
        public ObservableCollection<Vertex> Vertices 
        { 
            get;
            set;
        }
        public ObservableCollection<Edge> Edges
        {
            get;
            set;
        }
        public HandleDataPageViewModel()
        {
            Vertices = new ObservableCollection<Vertex>();
            Vertices.Add(new Vertex(30, 100));
            Vertices.Add(new Vertex(100, 200));
            Vertices.Add(new Vertex(150, 150));

            Edges = new ObservableCollection<Edge>();
            Edges.Add(new Edge(Vertices[0], Vertices[1]));
            Edges.Add(new Edge(Vertices[1], Vertices[2]));
            Edges.Add(new Edge(Vertices[0], Vertices[2]));
        }
    }
}
