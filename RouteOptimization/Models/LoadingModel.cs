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
    public class LoadingModel : ReactiveObject
    {
        private IShipmentsRepository _shipmentsRepository;
        private ICargosRepository _cargosRepository;
        private IRepository<Vehicle> _vehiclesRepository;
        private IRepository<Location> _locationsRepository;
        private IRoutesRepository _routesRepository;
        private IRepository<CargoAvailable> _cargoAvailableesRepository;
        private IRepository<ShipmentStatus> _shipmentStatusesRepository;
        private IRepository<VehicleStatus> _vehicleStatusesRepository;

        public LoadingModel(IShipmentsRepository shipmentsRepository, 
            ICargosRepository cargosRepository, 
            IRepository<Vehicle> vehiclesRepository, 
            IRepository<Location> locationsRepository,
            IRoutesRepository routesRepository,
            IRepository<CargoAvailable> cargoAvailableesRepository, 
            IRepository<ShipmentStatus> shipmentStatusesRepository,
            IRepository<VehicleStatus> vehiclesStatusesRepository)
        {
            _shipmentsRepository = shipmentsRepository;
            _cargosRepository = cargosRepository;
            _vehiclesRepository = vehiclesRepository;
            _locationsRepository = locationsRepository;
            _routesRepository = routesRepository;
            _cargoAvailableesRepository = cargoAvailableesRepository;
            _shipmentStatusesRepository = shipmentStatusesRepository;
            _vehicleStatusesRepository = vehiclesStatusesRepository;
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
        public async Task CreateShipmentsEditCargosVehicle(IEnumerable<Shipment> shipments, IEnumerable<Cargo> cargos, Vehicle vehicle)
        {
            await _shipmentsRepository.CreateShipmentsEditCargosVehicle(shipments, cargos, vehicle);
        }
        public async Task<RouteWay?> Navigate(Location? startLocation, Location? finishLocation)
        {
            if (startLocation != null && finishLocation != null)
            {
                RouteWay routeWay = await _routesRepository.GetRouteWay(startLocation.Id, finishLocation.Id);

                return routeWay;
            }
            return null;
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
        public async Task<IEnumerable<Cargo?>> GetCargosByLocationAvailable(Location location, CargoAvailable available )
        {
            return await _cargosRepository.GetAllByLocationAvailable(location, available);
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

        public async Task<IEnumerable<CargoAvailable?>> GetCargoAvailables()
        {
            return await _cargoAvailableesRepository.GetAll();
        }
        public async Task<CargoAvailable?> GetCargoAvailableByID(int id)
        {
            return await _cargoAvailableesRepository.GetByID(id);
        }

        public async Task<IEnumerable<ShipmentStatus?>> GetShipmentStatuses()
        {
            return await _shipmentStatusesRepository.GetAll();
        }
        public async Task<ShipmentStatus?> GetShipmentStatusByID(int id)
        {
            return await _shipmentStatusesRepository.GetByID(id);
        }

        public async Task<IEnumerable<VehicleStatus?>> GetVehicleStatuses()
        {
            return await _vehicleStatusesRepository.GetAll();
        }
        public async Task<VehicleStatus?> GetVehicleStatusByID(int id)
        {
            return await _vehicleStatusesRepository.GetByID(id);
        }
    }
}
