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
    public class VehiclesModel
    {
        IVehiclesRepository _repository;

        public VehiclesModel()
        {
            _repository = new SQLiteVehiclesRepository();
        }
        public async Task<IEnumerable<Vehicle?>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<Vehicle?> GetByID(int id)
        {
            return await _repository.GetByID(id);
        }
        public async Task<Vehicle?> Create(Vehicle entity)
        {
            return await _repository.Create(entity);
        }
        public async Task Edit(Vehicle entity)
        {
            await _repository.Edit(entity);
        }
        public async Task Delete(Vehicle entity)
        {
            await _repository.Delete(entity);
        }
    }
}
