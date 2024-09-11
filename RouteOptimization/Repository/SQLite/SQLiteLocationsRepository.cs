using Microsoft.EntityFrameworkCore;
using RouteOptimization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository.SQLite
{
    public class SQLiteLocationsRepository : ILocationsRepository
    {
        public async Task<ILocation?> Create(ILocation entity)
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Locations.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(ILocation entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Locations.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Edit(ILocation entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Locations.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ILocation?>> GetAll()
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Locations.LoadAsync();
            return context.Locations.Local.ToArray();
        }

        public async Task<ILocation?> GetByID(int id)
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.Locations.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
