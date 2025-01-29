using Microsoft.EntityFrameworkCore;
using RouteOptimization.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository.SQLite
{
    public class SQLiteVehicleStatusesRepository : IRepository<VehicleStatus>
    {
        public async Task<VehicleStatus?> Create(VehicleStatus entity)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(VehicleStatus entity)
        {
            throw new NotImplementedException();
        }

        public async Task Update(VehicleStatus entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.VehicleStatuses.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<VehicleStatus?>> GetAll()
        {
            using SQLiteContext context = new SQLiteContext();
            await context.VehicleStatuses.LoadAsync();
            return context.VehicleStatuses.Local.ToArray();
        }

        public async Task<IEnumerable<VehicleStatus?>> GetAll(int page, int pageSize = 10, string filter = "")
        {
            using SQLiteContext context = new SQLiteContext();
            await context.VehicleStatuses.Skip((page - 1) * pageSize).Take(pageSize).LoadAsync();
            return context.VehicleStatuses.ToArray();
        }

        public async Task<VehicleStatus?> GetByID(int id)
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.VehicleStatuses.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int?> Count()
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.VehicleStatuses.CountAsync();
        }
    }
}
