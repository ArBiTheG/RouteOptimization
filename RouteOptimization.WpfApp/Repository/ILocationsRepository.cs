using RouteOptimization.WpfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.Repository
{
    public interface ILocationsRepository
    {
        Task<IEnumerable<Location?>> GetAll();
        Task<Location?> GetByID(int id);
        Task<Location?> Create(Location entity);
        Task Edit(Location entity);
        Task Delete(Location entity);
    }
}
