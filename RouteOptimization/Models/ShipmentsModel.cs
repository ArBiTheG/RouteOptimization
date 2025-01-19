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
        private IShipmentRepository _shipmentsRepository;
        private ICargoRepository _cargosRepository;
        private IRepository<Vehicle> _vehiclesRepository;
        private IRepository<Location> _locationsRepository;
        private IRepository<ShipmentStatus> _statusesRepository;

        public ShipmentsModel(IShipmentRepository shipmentsRepository, ICargoRepository cargosRepository, IRepository<Vehicle> vehiclesRepository, IRepository<Location> locationsRepository, IRepository<ShipmentStatus> statusesRepository)
        {
            _shipmentsRepository = shipmentsRepository;
            _cargosRepository = cargosRepository;
            _vehiclesRepository = vehiclesRepository;
            _locationsRepository = locationsRepository;
            _statusesRepository = statusesRepository;
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
            await _shipmentsRepository.Update(entity);
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

        public async Task<IEnumerable<Cargo?>> GetCargos()
        {
            return await _cargosRepository.GetAll();
        }
        public async Task<Cargo?> GetCargoByID(int id)
        {
            return await _cargosRepository.GetByID(id);
        }

        public async Task<IEnumerable<Vehicle?>> GetVehicles()
        {
            return await _vehiclesRepository.GetAll();
        }
        public async Task<Vehicle?> GetVehicleByID(int id)
        {
            return await _vehiclesRepository.GetByID(id);
        }

        public async Task<IEnumerable<ShipmentStatus?>> GetStatuses()
        {
            return await _statusesRepository.GetAll();
        }
        public async Task<ShipmentStatus?> GetStatusByID(int id)
        {
            return await _statusesRepository.GetByID(id);
        }
    }
}
