using Microsoft.EntityFrameworkCore;
using RouteOptimization.WpfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.Repository.SQLite
{
    public class SQLiteVehicleTypesRepository: IVehicleTypesRepository
    {
        public async Task<VehicleType?> Create(VehicleType entity)
        {
            using SQLiteContext context = new SQLiteContext();
            await context.VehicleTypes.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(VehicleType entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.VehicleTypes.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Edit(VehicleType entity)
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

        public async Task<VehicleType?> GetByID(int id)
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.VehicleTypes.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
