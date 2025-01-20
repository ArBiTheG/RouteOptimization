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
    public class CargosModel : ReactiveObject
    {
        private ICargosRepository _cargoesRepository;
        private IRepository<CargoAvailable> _cargoAvailablesRepository;
        private IRepository<Location> _locationsRepository;

        public CargosModel(ICargosRepository cargoesRepository, IRepository<CargoAvailable> cargoAvailablesRepository, IRepository<Location> locationsRepository)
        {
            _cargoesRepository = cargoesRepository;
            _cargoAvailablesRepository = cargoAvailablesRepository;
            _locationsRepository = locationsRepository;
        }

        public async Task<IEnumerable<Cargo?>> GetAll()
        {
            return await _cargoesRepository.GetAll();
        }
        public async Task<Cargo?> GetByID(int id)
        {
            return await _cargoesRepository.GetByID(id);
        }
        public async Task<Cargo?> Create(Cargo entity)
        {
            return await _cargoesRepository.Create(entity);
        }
        public async Task Edit(Cargo entity)
        {
            await _cargoesRepository.Update(entity);
        }
        public async Task Delete(Cargo entity)
        {
            await _cargoesRepository.Delete(entity);
        }
        public async Task<IEnumerable<CargoAvailable?>> GetAvailables()
        {
            return await _cargoAvailablesRepository.GetAll();
        }
        public async Task<CargoAvailable?> GetAvailableByID(int id)
        {
            return await _cargoAvailablesRepository.GetByID(id);
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
