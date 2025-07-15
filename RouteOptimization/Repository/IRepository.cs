using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T?>> GetAll();
        Task<IEnumerable<T?>> GetAll(int page, int pageSize = 10, string filter = "");
        Task<T?> GetByID(int id);
        Task<T?> Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<int?> Count();
    }
}
