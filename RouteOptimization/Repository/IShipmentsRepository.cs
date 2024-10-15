using RouteOptimization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository
{
    public interface IShipmentsRepository
    {
        Task<IEnumerable<Shipment?>> GetAll();
        Task<Shipment?> GetByID(int id);
        Task<Shipment?> Create(Shipment entity);
        Task Edit(Shipment entity);
        Task Delete(Shipment entity);
    }
}
