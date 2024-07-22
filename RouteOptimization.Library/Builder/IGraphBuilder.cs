using RouteOptimization.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Library.Builder
{
    public interface IGraphBuilder
    {
        public IGraphBuilder AddVertex(int id);

        public IGraphBuilder AddEdge(int beginId, int endId, double weight);
        public Graph Build();
    }
}
