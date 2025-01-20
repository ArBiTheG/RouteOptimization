using RouteOptimization.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository
{
    public interface ICargosRepository: IRepository<Cargo>
    {
        Task<IEnumerable<Cargo?>> GetAllByLocationAvailable(Location location, CargoAvailable available);
    }
}
