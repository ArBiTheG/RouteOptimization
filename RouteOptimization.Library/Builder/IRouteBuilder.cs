using RouteOptimization.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Library.Builder
{
    public interface IRouteBuilder
    {
        IRouteBuilder SetBegin(int id);
        IRouteBuilder SetEnd(int id);
        Route Build();
    }
}
