﻿using Microsoft.EntityFrameworkCore;
using RouteOptimization.WpfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.Repository.SQLite
{
    public class SQLiteShipmentsRepository : IShipmentsRepository
    {
        public async Task<Shipment?> Create(Shipment entity)
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Shipments.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(Shipment entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Shipments.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Edit(Shipment entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Shipments.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Shipment?>> GetAll()
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Shipments.LoadAsync();
            return context.Shipments.Include(u => u.Origin).Include(u => u.Destination).ToArray();
        }

        public async Task<Shipment?> GetByID(int id)
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.Shipments.Include(u => u.Origin).Include(u => u.Destination).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
