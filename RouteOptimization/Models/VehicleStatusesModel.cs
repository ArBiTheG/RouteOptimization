using RouteOptimization.Models.Entities;
using RouteOptimization.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models
{
    public class VehicleStatusesModel
    {
        IVehicleStatusesRepository _repository;

        public async Task<IEnumerable<VehicleStatus?>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<VehicleStatus?> GetByID(int id)
        {
            return await _repository.GetByID(id);
        }
        public async Task<VehicleStatus?> Create(VehicleStatus entity)
        {
            return await _repository.Create(entity);
        }
        public async Task Edit(VehicleStatus entity)
        {
            await _repository.Edit(entity);
        }
        public async Task Delete(VehicleStatus entity)
        {
            await _repository.Delete(entity);
        }
    }
}
