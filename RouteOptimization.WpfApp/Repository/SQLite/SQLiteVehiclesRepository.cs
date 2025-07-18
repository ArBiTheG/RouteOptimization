﻿using Microsoft.EntityFrameworkCore;
using RouteOptimization.WpfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.Repository.SQLite
{
    public class SQLiteVehiclesRepository : IVehiclesRepository
    {
        public async Task<Vehicle?> Create(Vehicle entity)
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Vehicles.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(Vehicle entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Vehicles.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Edit(Vehicle entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Vehicles.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Vehicle?>> GetAll()
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Vehicles.LoadAsync();
            return context.Vehicles.Include(u => u.Status).Include(u => u.Type).ToArray();
        }

        public async Task<Vehicle?> GetByID(int id)
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.Vehicles.Include(u => u.Status).Include(u => u.Type).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
