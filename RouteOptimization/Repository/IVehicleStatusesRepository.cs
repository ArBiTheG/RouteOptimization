using RouteOptimization.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository
{
    public interface IVehicleStatusesRepository
    {
        Task<IEnumerable<VehicleStatus?>> GetAll();
        Task<VehicleStatus?> GetByID(int id);
        Task<VehicleStatus?> Create(VehicleStatus entity);
        Task Edit(VehicleStatus entity);
        Task Delete(VehicleStatus entity);
    }
}
