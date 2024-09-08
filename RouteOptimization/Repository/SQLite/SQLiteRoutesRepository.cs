using Microsoft.EntityFrameworkCore;
using RouteOptimization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository.SQLite
{
    public class SQLiteRoutesRepository : IRoutesRepository
    {
        public async Task<IRoute?> Create(IRoute entity)
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Routes.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(IRoute entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Routes.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Edit(IRoute entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Routes.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<IRoute?>> GetAll()
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Routes.LoadAsync();
            return context.Routes.Local.ToArray();
        }

        public async Task<IRoute?> GetByID(int id)
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.Routes.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
