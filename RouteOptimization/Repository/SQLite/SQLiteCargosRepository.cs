using Microsoft.EntityFrameworkCore;
using RouteOptimization.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository.SQLite
{
    public class SQLiteCargosRepository : IRepository<Cargo>
    {
        public async Task<int?> Count()
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.Cargos.CountAsync();
        }

        public async Task<Cargo?> Create(Cargo entity)
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Cargos.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(Cargo entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Cargos.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Cargo?>> GetAll()
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Cargos.LoadAsync();
            return context.Cargos.ToArray();
        }

        public async Task<IEnumerable<Cargo?>> GetAll(int page, int pageSize = 10, string filter = "")
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Cargos.Skip((page - 1) * pageSize).Take(pageSize).LoadAsync();
            return context.Cargos.ToArray();
        }

        public async Task<Cargo?> GetByID(int id)
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.Cargos.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Update(Cargo entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Cargos.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
