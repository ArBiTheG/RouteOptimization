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
        private IRepository<Vehicle> _vehiclesRepository;
        private IRepository<VehicleType> _vehicleTypesRepository;
        private IRepository<VehicleStatus> _vehicleStatusesRepository;

        public VehiclesModel()
        {
            _vehiclesRepository = new SQLiteVehiclesRepository();
            _vehicleTypesRepository = new SQLiteVehicleTypesRepository();
            _vehicleStatusesRepository = new SQLiteVehicleStatusesRepository();
        }
        public async Task<IEnumerable<Vehicle?>> GetAll()
        {
            return await _vehiclesRepository.GetAll();
        }
        public async Task<Vehicle?> GetByID(int id)
        {
            return await _vehiclesRepository.GetByID(id);
        }
        public async Task<Vehicle?> Create(Vehicle entity)
        {
            return await _vehiclesRepository.Create(entity);
        }
        public async Task Edit(Vehicle entity)
        {
            await _vehiclesRepository.Update(entity);
        }
        public async Task Delete(Vehicle entity)
        {
            await _vehiclesRepository.Delete(entity);
        }

        public async Task<IEnumerable<VehicleType?>> GetTypes()
        {
            return await _vehicleTypesRepository.GetAll();
        }
        public async Task<VehicleType?> GetTypeByID(int id)
        {
            return await _vehicleTypesRepository.GetByID(id);
        }

        public async Task<IEnumerable<VehicleStatus?>> GetStatuses()
        {
            return await _vehicleStatusesRepository.GetAll();
        }
        public async Task<VehicleStatus?> GetStatusByID(int id)
        {
            return await _vehicleStatusesRepository.GetByID(id);
        }
    }
}
