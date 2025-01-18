using Microsoft.EntityFrameworkCore;
using RouteOptimization.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository.SQLite
{
    public class SQLiteCargoAvailablesRepository : IRepository<CargoAvailable>
    {
        public async Task<int?> Count()
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.CargoAvailables.CountAsync();
        }

        public async Task<CargoAvailable?> Create(CargoAvailable entity)
        {
            using SQLiteContext context = new SQLiteContext();
            await context.CargoAvailables.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(CargoAvailable entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.CargoAvailables.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CargoAvailable?>> GetAll()
        {
            using SQLiteContext context = new SQLiteContext();
            await context.CargoAvailables.LoadAsync();
            return context.CargoAvailables.ToArray();
        }

        public async Task<IEnumerable<CargoAvailable?>> GetAll(int page, int pageSize = 10, string filter = "")
        {
            using SQLiteContext context = new SQLiteContext();
            await context.CargoAvailables.Skip((page - 1) * pageSize).Take(pageSize).LoadAsync();
            return context.CargoAvailables.ToArray();
        }

        public async Task<CargoAvailable?> GetByID(int id)
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.CargoAvailables.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Update(CargoAvailable entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.CargoAvailables.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
