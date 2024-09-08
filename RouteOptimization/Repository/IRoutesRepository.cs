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
        Task<IEnumerable<IRoute?>> GetAll();
        Task<IRoute?> GetByID(int id);
        Task<IRoute?> Create(IRoute entity);
        Task Edit(IRoute entity);
        Task Delete(IRoute entity);
    }
}
