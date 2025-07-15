using RouteOptimization.Models.Entities;
using RouteOptimization.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models
{
    public class WarehouseModel
    {
        private ICargosRepository _cargosRepository;
        private IRepository<Location> _locationsRepository;

        public WarehouseModel(ICargosRepository cargosRepository, IRepository<Location> locationsRepository)
        {
            _cargosRepository = cargosRepository;
            _locationsRepository = locationsRepository;
        }

        public async Task<IEnumerable<Location?>> GetLocations()
        {
            return await _locationsRepository.GetAll();
        }
        public async Task<Location?> GetLocationByID(int id)
        {
            return await _locationsRepository.GetByID(id);
        }
        public async Task<IEnumerable<Cargo?>> GetAll()
        {
            return await _cargosRepository.GetAll();
        }
        public async Task<Cargo?> GetByID(int id)
        {
            return await _cargosRepository.GetByID(id);
        }
        public async Task<Cargo?> Create(Cargo entity, Location place)
        {
            entity.LocationId = place.Id;
            entity.AvailableId = CargoAvailableValue.Bought.Id;
            return await _cargosRepository.Create(entity);
        }
        public async Task Edit(Cargo entity)
        {
            await _cargosRepository.Update(entity);
        }
        public async Task Sell(Cargo entity)
        {
            entity.AvailableId = CargoAvailableValue.Bought.Id;
            await _cargosRepository.Update(entity);
        }
        public async Task<IEnumerable<Cargo?>> GetCargosByLocationAvailable(Location location, int available_id )
        {
            return await _cargosRepository.GetAllByLocationAvailable(location, available_id);
        }
    }
}
