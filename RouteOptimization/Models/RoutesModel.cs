using RouteOptimization.Models.Entities;
using RouteOptimization.Repository;
using RouteOptimization.Repository.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models
{
    public class RoutesModel
    {
        IRoutesRepository _repository;

        public RoutesModel()
        {
            _repository = new SQLiteRoutesRepository();
        }
        public async Task<IEnumerable<Route?>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<Route?> GetByID(int id)
        {
            return await _repository.GetByID(id);
        }
        public async Task<Route?> Create(Route entity)
        {
            return await _repository.Create(entity);
        }
        public async Task Edit(Route entity)
        {
            await _repository.Edit(entity);
        }
        public async Task Delete(Route entity)
        {
            await _repository.Delete(entity);
        }
    }
}
