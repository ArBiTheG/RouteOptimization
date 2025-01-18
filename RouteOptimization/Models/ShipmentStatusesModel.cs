using ReactiveUI;
using RouteOptimization.Models.Entities;
using RouteOptimization.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models
{
    public class ShipmentStatusesModel : ReactiveObject
    {
        private IRepository<ShipmentStatus> _shipmentStatusesRepository;

        public ShipmentStatusesModel(IRepository<ShipmentStatus> shipmentStatusesRepository)
        {
            _shipmentStatusesRepository = shipmentStatusesRepository;
        }

        public async Task<IEnumerable<ShipmentStatus?>> GetAll()
        {
            return await _shipmentStatusesRepository.GetAll();
        }
        public async Task<ShipmentStatus?> GetByID(int id)
        {
            return await _shipmentStatusesRepository.GetByID(id);
        }
        public async Task<ShipmentStatus?> Create(ShipmentStatus entity)
        {
            return await _shipmentStatusesRepository.Create(entity);
        }
        public async Task Edit(ShipmentStatus entity)
        {
            await _shipmentStatusesRepository.Update(entity);
        }
        public async Task Delete(ShipmentStatus entity)
        {
            await _shipmentStatusesRepository.Delete(entity);
        }
    }
}
