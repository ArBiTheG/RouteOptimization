using RouteOptimization.Models.Entities;
using RouteOptimization.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models
{
    public class HandlingModel
    {
        private IShipmentsRepository _shipmentsRepository;
        public HandlingModel(IShipmentsRepository shipmentsRepository)
        {
            _shipmentsRepository = shipmentsRepository;
        }

        public async Task<IEnumerable<Shipment?>> GetShipments()
        {
            return await _shipmentsRepository.GetUncompleteShipments();
        }
        public async Task Apply(Shipment shipment)
        {
            await _shipmentsRepository.AcceptShipment(shipment);
        }
        public async Task Cancel(Shipment shipment)
        {
            await _shipmentsRepository.CancelShipment(shipment);
        }
    }
}
