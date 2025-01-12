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
    public class VehicleStatusesModel
    {
        private IRepository<VehicleStatus> _vehicleStatusesRepository;

        public VehicleStatusesModel()
        {
            _vehicleStatusesRepository = new SQLiteVehicleStatusesRepository();
        }
        public async Task<IEnumerable<VehicleStatus?>> GetAll()
        {
            return await _vehicleStatusesRepository.GetAll();
        }
        public async Task<VehicleStatus?> GetByID(int id)
        {
            return await _vehicleStatusesRepository.GetByID(id);
        }
        public async Task<VehicleStatus?> Create(VehicleStatus entity)
        {
            return await _vehicleStatusesRepository.Create(entity);
        }
        public async Task Edit(VehicleStatus entity)
        {
            await _vehicleStatusesRepository.Update(entity);
        }
        public async Task Delete(VehicleStatus entity)
        {
            await _vehicleStatusesRepository.Delete(entity);
        }
    }
}
