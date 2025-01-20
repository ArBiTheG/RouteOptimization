using RouteOptimization.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository
{
    public interface IRoutesRepository: IRepository<Route>
    {
        Task<RouteWay> GetRouteWay(int startLocationId, int finishLocationId);
    }
}
