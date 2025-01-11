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
    public class VehicleTypesModel
    {
        IVehicleTypesRepository _repository;

        public VehicleTypesModel()
        {
            _repository = new SQLiteVehicleTypesRepository();
        }
        public async Task<IEnumerable<VehicleType?>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<VehicleType?> GetByID(int id)
        {
            return await _repository.GetByID(id);
        }
        public async Task<VehicleType?> Create(VehicleType entity)
        {
            return await _repository.Create(entity);
        }
        public async Task Edit(VehicleType entity)
        {
            await _repository.Edit(entity);
        }
        public async Task Delete(VehicleType entity)
        {
            await _repository.Delete(entity);
        }
    }
}
