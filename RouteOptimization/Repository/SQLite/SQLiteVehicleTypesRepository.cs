using Microsoft.EntityFrameworkCore;
using RouteOptimization.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository.SQLite
{
    public class SQLiteVehicleTypesRepository : IRepository<VehicleType>
    {
        public async Task<VehicleType?> Create(VehicleType entity)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(VehicleType entity)
        {
            throw new NotImplementedException();
        }

        public async Task Update(VehicleType entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.VehicleTypes.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<VehicleType?>> GetAll()
        {
            using SQLiteContext context = new SQLiteContext();
            await context.VehicleTypes.LoadAsync();
            return context.VehicleTypes.Local.ToArray();
        }

        public async Task<IEnumerable<VehicleType?>> GetAll(int page, int pageSize = 10, string filter = "")
        {
            using SQLiteContext context = new SQLiteContext();
            await context.VehicleTypes.Skip((page - 1) * pageSize).Take(pageSize).LoadAsync();
            return context.VehicleTypes.ToArray();
        }

        public async Task<VehicleType?> GetByID(int id)
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.VehicleTypes.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int?> Count()
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.VehicleTypes.CountAsync();
        }
    }
}
