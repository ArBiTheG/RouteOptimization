using Microsoft.EntityFrameworkCore;
using RouteOptimization.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository.SQLite
{
    public class SQLiteRoutesRepository : IRepository<Route>
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

        public async Task Update(Route entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Routes.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Route?>> GetAll()
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Routes.Include(l => l.StartLocation).Include(l => l.FinishLocation).LoadAsync();
            return context.Routes.Local.ToArray();
        }

        public async Task<IEnumerable<Route?>> GetAll(int page, int pageSize = 10, string filter = "")
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Routes.Skip((page - 1) * pageSize).Take(pageSize).LoadAsync();
            return context.Routes.ToArray();
        }

        public async Task<Route?> GetByID(int id)
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.Routes.Include(l => l.StartLocation).Include(l => l.FinishLocation).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int?> Count()
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.Routes.CountAsync();
        }
    }
}
