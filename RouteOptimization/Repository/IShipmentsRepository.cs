using RouteOptimization.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository
{
    public interface IShipmentsRepository: IRepository<Shipment>
    {
        Task CreateShipmentsEditCargosVehicle(IEnumerable<Shipment> shipments, IEnumerable<Cargo> cargos, Vehicle vehicle);
    }
}
