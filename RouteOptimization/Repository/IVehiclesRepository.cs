using RouteOptimization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository
{
    public interface IVehiclesRepository
    {
        Task<IEnumerable<IVehicle?>> GetAll();
        Task<IVehicle?> GetByID(int id);
        Task<IVehicle?> Create(IVehicle entity);
        Task Edit(IVehicle entity);
        Task Delete(IVehicle entity);
    }
}
