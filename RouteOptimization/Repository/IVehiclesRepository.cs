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
        Task<IEnumerable<Vehicle?>> GetAll();
        Task<Vehicle?> GetByID(int id);
        Task<Vehicle?> Create(Vehicle entity);
        Task Edit(Vehicle entity);
        Task Delete(Vehicle entity);
    }
}
