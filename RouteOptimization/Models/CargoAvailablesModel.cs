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
    public class CargoAvailablesModel : ReactiveObject
    {
        private IRepository<CargoAvailable> _cargoAvailablesRepository;

        public CargoAvailablesModel(IRepository<CargoAvailable> cargoAvailablesRepository)
        {
            _cargoAvailablesRepository = cargoAvailablesRepository;
        }

        public async Task<IEnumerable<CargoAvailable?>> GetAll()
        {
            return await _cargoAvailablesRepository.GetAll();
        }
        public async Task<CargoAvailable?> GetByID(int id)
        {
            return await _cargoAvailablesRepository.GetByID(id);
        }
        public async Task<CargoAvailable?> Create(CargoAvailable entity)
        {
            return await _cargoAvailablesRepository.Create(entity);
        }
        public async Task Edit(CargoAvailable entity)
        {
            await _cargoAvailablesRepository.Update(entity);
        }
        public async Task Delete(CargoAvailable entity)
        {
            await _cargoAvailablesRepository.Delete(entity);
        }
    }
}
