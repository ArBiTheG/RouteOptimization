using RouteOptimization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository
{
    public interface ICarsRepository
    {
        Task<IEnumerable<ICar?>> GetAll();
        Task<ICar?> GetByID(int id);
        Task<ICar?> Create(ICar entity);
        Task Edit(ICar entity);
        Task Delete(ICar entity);
    }
}
