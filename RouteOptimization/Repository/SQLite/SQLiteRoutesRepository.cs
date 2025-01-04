using Microsoft.EntityFrameworkCore;
using RouteOptimization.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository.SQLite
{
    public class SQLiteRoutesRepository : IRoutesRepository
    {
        public async Task<Route?> Create(Route entity)
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Routes.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(Route entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Routes.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Edit(Route entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Routes.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Route?>> GetAll()
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Routes.LoadAsync();
            return context.Routes.Local.ToArray();
        }

        public async Task<Route?> GetByID(int id)
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.Routes.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
