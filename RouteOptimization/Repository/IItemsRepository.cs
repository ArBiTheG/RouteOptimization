using RouteOptimization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository
{
    public interface IItemsRepository
    {
        Task<IEnumerable<IItem?>> GetAll();
        Task<IItem?> GetByID(int id);
        Task<IItem?> Create(IItem entity);
        Task Edit(IItem entity);
        Task Delete(IItem entity);
    }
}
