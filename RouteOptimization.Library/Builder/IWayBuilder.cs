using RouteOptimization.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Library.Builder
{
    public interface IWayBuilder
    {
        IWayBuilder SetBegin(int id);
        IWayBuilder SetEnd(int id);
        Way Build();
        Task<Way> BuildAsync();
    }
}
