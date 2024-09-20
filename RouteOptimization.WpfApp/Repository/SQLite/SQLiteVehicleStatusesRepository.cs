using Microsoft.EntityFrameworkCore;
using RouteOptimization.WpfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.Repository.SQLite
{
    public class SQLiteVehicleStatusesRepository : IVehicleStatusesRepository
    {
        public async Task<VehicleStatus?> Create(VehicleStatus entity)
        {
            using SQLiteContext context = new SQLiteContext();
            await context.VehicleStatuses.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(VehicleStatus entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.VehicleStatuses.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Edit(VehicleStatus entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.VehicleStatuses.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<VehicleStatus?>> GetAll()
        {
            using SQLiteContext context = new SQLiteContext();
            await context.VehicleStatuses.LoadAsync();
            return context.VehicleStatuses.ToArray();
        }

        public async Task<VehicleStatus?> GetByID(int id)
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.VehicleStatuses.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
