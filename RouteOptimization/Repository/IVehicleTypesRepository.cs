using RouteOptimization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository
{
    public interface IVehicleTypesRepository
    {
        Task<IEnumerable<IVehicleType?>> GetAll();
        Task<IVehicleType?> GetByID(int id);
        Task<IVehicleType?> Create(IVehicleType entity);
        Task Edit(IVehicleType entity);
        Task Delete(IVehicleType entity);
    }
}
