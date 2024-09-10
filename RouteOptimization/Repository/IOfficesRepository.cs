using RouteOptimization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository
{
    public interface IOfficesRepository
    {
        Task<IEnumerable<ILocation?>> GetAll();
        Task<ILocation?> GetByID(int id);
        Task<ILocation?> Create(ILocation entity);
        Task Edit(ILocation entity);
        Task Delete(ILocation entity);
    }
}
