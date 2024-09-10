using Microsoft.EntityFrameworkCore;
using RouteOptimization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository.SQLite
{
    public class SQLiteOfficesRepository : IOfficesRepository
    {
        public async Task<ILocation?> Create(ILocation entity)
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Offices.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(ILocation entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Offices.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Edit(ILocation entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Offices.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ILocation?>> GetAll()
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Offices.LoadAsync();
            return context.Offices.Local.ToArray();
        }

        public async Task<ILocation?> GetByID(int id)
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.Offices.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
