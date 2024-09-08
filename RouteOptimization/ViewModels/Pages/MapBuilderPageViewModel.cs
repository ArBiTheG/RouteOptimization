using RouteOptimization.Controls.MapBuilder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages
{
    public class MapBuilderPageViewModel: ViewModelBase
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

        private static Scene _scene = new Scene(0, 0);

        public Scene Scene { get => _scene; }

        public MapBuilderPageViewModel()
        {
            Vertices = new ObservableCollection<Vertex>();

            Edges = new ObservableCollection<Edge>();
        }
    }
}
