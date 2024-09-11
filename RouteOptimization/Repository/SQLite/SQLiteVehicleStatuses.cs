using Microsoft.EntityFrameworkCore;
using RouteOptimization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository.SQLite
{
    public class SQLiteVehicleStatuses : IVehicleStatusesRepository
    {
        public async Task<IVehicleStatus?> Create(IVehicleStatus entity)
        {
            using SQLiteContext context = new SQLiteContext();
            await context.VehicleStatuses.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(IVehicleStatus entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.VehicleStatuses.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Edit(IVehicleStatus entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.VehicleStatuses.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<IVehicleStatus?>> GetAll()
        {
            using SQLiteContext context = new SQLiteContext();
            await context.VehicleStatuses.LoadAsync();
            return context.VehicleStatuses.Local.ToArray();
        }

        public async Task<IVehicleStatus?> GetByID(int id)
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.VehicleStatuses.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
