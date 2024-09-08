using RouteOptimization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository
{
    public interface IOfficesRepository
    {
        Task<IEnumerable<IOffice?>> GetAll();
        Task<IOffice?> GetByID(int id);
        Task<IOffice?> Create(IOffice entity);
        Task Edit(IOffice entity);
        Task Delete(IOffice entity);
    }
}
