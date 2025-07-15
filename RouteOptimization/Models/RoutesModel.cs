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
    public class RoutesModel
    {
        private IRoutesRepository _routesRepository;
        private IRepository<Location> _locationsRepository;

        public RoutesModel(IRoutesRepository routesRepository, IRepository<Location> locationsRepository)
        {
            _routesRepository = routesRepository;
            _locationsRepository = locationsRepository;
        }

        public async Task<IEnumerable<Route?>> GetAll()
        {
            return await _routesRepository.GetAll();
        }

        public async Task<Route?> GetByID(int id)
        {
            return await _routesRepository.GetByID(id);
        }
        public async Task<Route?> Create(Route entity)
        {
            return await _routesRepository.Create(entity);
        }
        public async Task Edit(Route entity)
        {
            await _routesRepository.Update(entity);
        }
        public async Task Delete(Route entity)
        {
            await _routesRepository.Delete(entity);
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
