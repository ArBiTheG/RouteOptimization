using Microsoft.EntityFrameworkCore;
using RouteOptimization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository.SQLite
{
    public class SQLiteItemsRepository : IItemsRepository
    {
        public async Task<IItem?> Create(IItem entity)
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Items.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(IItem entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Items.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Edit(IItem entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Items.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<IItem?>> GetAll()
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Items.LoadAsync();
            return context.Items.Local.ToArray();
        }

        public async Task<IItem?> GetByID(int id)
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.Items.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
