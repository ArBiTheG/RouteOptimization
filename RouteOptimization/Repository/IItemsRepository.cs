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
        Task<IEnumerable<IShipment?>> GetAll();
        Task<IShipment?> GetByID(int id);
        Task<IShipment?> Create(IShipment entity);
        Task Edit(IShipment entity);
        Task Delete(IShipment entity);
    }
}
