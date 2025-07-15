using Microsoft.EntityFrameworkCore;
using RouteOptimization.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository.SQLite
{
    public class SQLiteShipmentStatusesRepository : IRepository<ShipmentStatus>
    {
        public async Task<int?> Count()
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.ShipmentStatuses.CountAsync();
        }

        public async Task<ShipmentStatus?> Create(ShipmentStatus entity)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(ShipmentStatus entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ShipmentStatus?>> GetAll()
        {
            using SQLiteContext context = new SQLiteContext();
            await context.ShipmentStatuses.LoadAsync();
            return context.ShipmentStatuses.ToArray();
        }

        public async Task<IEnumerable<ShipmentStatus?>> GetAll(int page, int pageSize = 10, string filter = "")
        {
            using SQLiteContext context = new SQLiteContext();
            await context.ShipmentStatuses.Skip((page - 1) * pageSize).Take(pageSize).LoadAsync();
            return context.ShipmentStatuses.ToArray();
        }

        public async Task<ShipmentStatus?> GetByID(int id)
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.ShipmentStatuses.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Update(ShipmentStatus entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.ShipmentStatuses.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
