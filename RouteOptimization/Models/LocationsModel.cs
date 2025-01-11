using ReactiveUI;
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
    public class LocationsModel: ReactiveObject
    {
        ILocationsRepository _repository;

        public LocationsModel()
        {
            _repository = new SQLiteLocationsRepository();
        }
        public async Task<IEnumerable<Location?>> GetAll()
        {
            return await _repository.GetAll(); 
        }
        public async Task<Location?> GetByID(int id)
        {
            return await _repository.GetByID(id);
        }
        public async Task<Location?> Create(Location entity)
        {
            return await _repository.Create(entity);
        }
        public async Task Edit(Location entity) 
        {
            await _repository.Edit(entity);
        }
        public async Task Delete(Location entity)
        {
            await _repository.Delete(entity);
        }
    }
}
