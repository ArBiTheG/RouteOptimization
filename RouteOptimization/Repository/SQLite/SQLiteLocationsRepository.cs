using Microsoft.EntityFrameworkCore;
using RouteOptimization.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository.SQLite
{
    public class SQLiteLocationsRepository : IRepository<Location>
    {
        public async Task<Location?> Create(Location entity)
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Locations.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(Location entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Locations.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Update(Location entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Locations.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Location?>> GetAll()
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Locations.LoadAsync();
            return context.Locations.ToArray();
        }

        public async Task<IEnumerable<Location?>> GetAll(int page, int pageSize = 10, string filter = "")
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Locations.Skip((page - 1) * pageSize).Take(pageSize).LoadAsync();
            return context.Locations.ToArray();
        }

        public async Task<Location?> GetByID(int id)
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.Locations.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int?> Count()
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.Locations.CountAsync();
        }
    }
}
