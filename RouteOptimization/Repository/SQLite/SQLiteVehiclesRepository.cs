using Microsoft.EntityFrameworkCore;
using RouteOptimization.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository.SQLite
{
    public class SQLiteVehiclesRepository : IRepository<Vehicle>
    {
        public async Task<Vehicle?> Create(Vehicle entity)
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Vehicles.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(Vehicle entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Vehicles.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Update(Vehicle entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Vehicles.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Vehicle?>> GetAll()
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Vehicles.LoadAsync();
            return context.Vehicles.Local.ToArray();
        }

        public async Task<IEnumerable<Vehicle?>> GetAll(int page, int pageSize = 10, string filter = "")
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Vehicles.Skip((page - 1) * pageSize).Take(pageSize).LoadAsync();
            return context.Vehicles.ToArray();
        }

        public async Task<Vehicle?> GetByID(int id)
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.Vehicles.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int?> Count()
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.Vehicles.CountAsync();
        }
    }
}
