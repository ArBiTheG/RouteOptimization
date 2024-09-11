using RouteOptimization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository
{
    public interface IVehicleStatusesRepository
    {
        Task<IEnumerable<IVehicleStatus?>> GetAll();
        Task<IVehicleStatus?> GetByID(int id);
        Task<IVehicleStatus?> Create(IVehicleStatus entity);
        Task Edit(IVehicleStatus entity);
        Task Delete(IVehicleStatus entity);
    }
}
