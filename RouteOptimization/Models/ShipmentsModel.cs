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
    public class ShipmentsModel
    {
        IShipmentsRepository _shipmentsRepository;
        ILocationsRepository _locationsRepository;

        public ShipmentsModel()
        {
            _shipmentsRepository = new SQLiteShipmentsRepository();
            _locationsRepository = new SQLiteLocationsRepository();
        }
        public async Task<IEnumerable<Shipment?>> GetAll()
        {
            return await _shipmentsRepository.GetAll();
        }
        public async Task<Shipment?> GetByID(int id)
        {
            return await _shipmentsRepository.GetByID(id);
        }
        public async Task<Shipment?> Create(Shipment entity)
        {
            return await _shipmentsRepository.Create(entity);
        }
        public async Task Edit(Shipment entity)
        {
            await _shipmentsRepository.Edit(entity);
        }
        public async Task Delete(Shipment entity)
        {
            await _shipmentsRepository.Delete(entity);
        }

        public async Task<IEnumerable<Location?>> GetLocations()
        {
            return await _locationsRepository.GetAll();
        }
        public async Task<Location?> GetLocationByID(int id)
        {
            return await _locationsRepository.GetByID(id);
        }
    }
}
