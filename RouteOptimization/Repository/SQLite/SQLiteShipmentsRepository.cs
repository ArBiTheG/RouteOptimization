using Microsoft.EntityFrameworkCore;
using RouteOptimization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository.SQLite
{
    public class SQLiteShipmentsRepository : IShipmentsRepository
    {
        public async Task<IShipment?> Create(IShipment entity)
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Shipments.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(IShipment entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Shipments.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Edit(IShipment entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Shipments.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<IShipment?>> GetAll()
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Shipments.LoadAsync();
            return context.Shipments.Local.ToArray();
        }

        public async Task<IShipment?> GetByID(int id)
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.Shipments.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
