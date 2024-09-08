using Microsoft.EntityFrameworkCore;
using RouteOptimization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository.SQLite
{
    public class SQLiteCarsRepository : ICarsRepository
    {
        public async Task<ICar?> Create(ICar entity)
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Cars.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(ICar entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Cars.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Edit(ICar entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Cars.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ICar?>> GetAll()
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Cars.LoadAsync();
            return context.Cars.Local.ToArray();
        }

        public async Task<ICar?> GetByID(int id)
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.Cars.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
