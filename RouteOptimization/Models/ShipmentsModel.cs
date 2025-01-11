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
        IShipmentsRepository _repository;

        public ShipmentsModel()
        {
            _repository = new SQLiteShipmentsRepository();
        }
        public async Task<IEnumerable<Shipment?>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<Shipment?> GetByID(int id)
        {
            return await _repository.GetByID(id);
        }
        public async Task<Shipment?> Create(Shipment entity)
        {
            return await _repository.Create(entity);
        }
        public async Task Edit(Shipment entity)
        {
            await _repository.Edit(entity);
        }
        public async Task Delete(Shipment entity)
        {
            await _repository.Delete(entity);
        }
    }
}
