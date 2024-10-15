using RouteOptimization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository
{
    public interface IRoutesRepository
    {
        Task<IEnumerable<Route?>> GetAll();
        Task<Route?> GetByID(int id);
        Task<Route?> Create(Route entity);
        Task Edit(Route entity);
        Task Delete(Route entity);
    }
}
