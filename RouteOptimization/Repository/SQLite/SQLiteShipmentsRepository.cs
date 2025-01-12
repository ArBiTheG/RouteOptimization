using Microsoft.EntityFrameworkCore;
using RouteOptimization.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository.SQLite
{
    public class SQLiteShipmentsRepository : IRepository<Shipment>
    {
        public async Task<Shipment?> Create(Shipment entity)
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Shipments.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(Shipment entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Shipments.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Update(Shipment entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Shipments.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Shipment?>> GetAll()
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Shipments.LoadAsync();
            return context.Shipments.Local.ToArray();
        }

        public async Task<IEnumerable<Shipment?>> GetAll(int page, int pageSize = 10, string filter = "")
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Shipments.Skip((page - 1) * pageSize).Take(pageSize).LoadAsync();
            return context.Shipments.ToArray();
        }

        public async Task<Shipment?> GetByID(int id)
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.Shipments.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int?> Count()
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.Shipments.CountAsync();
        }
    }
}
