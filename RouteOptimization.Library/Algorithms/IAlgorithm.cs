using RouteOptimization.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Library.Algorithms
{
    public interface IAlgorithm
    {
        Route BuildTo(Vertex vertexBegin, Vertex vertexEnd);
    }
}
