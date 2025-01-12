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
        private IRepository<VehicleType> _vehicleTypesRepository;

        public VehicleTypesModel()
        {
            _vehicleTypesRepository = new SQLiteVehicleTypesRepository();
        }
        public async Task<IEnumerable<VehicleType?>> GetAll()
        {
            return await _vehicleTypesRepository.GetAll();
        }
        public async Task<VehicleType?> GetByID(int id)
        {
            return await _vehicleTypesRepository.GetByID(id);
        }
        public async Task<VehicleType?> Create(VehicleType entity)
        {
            return await _vehicleTypesRepository.Create(entity);
        }
        public async Task Edit(VehicleType entity)
        {
            await _vehicleTypesRepository.Update(entity);
        }
        public async Task Delete(VehicleType entity)
        {
            await _vehicleTypesRepository.Delete(entity);
        }
    }
}
