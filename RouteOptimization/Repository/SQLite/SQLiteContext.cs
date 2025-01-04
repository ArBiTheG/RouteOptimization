using Microsoft.EntityFrameworkCore;
using RouteOptimization.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository.SQLite
{
    public class SQLiteContext: DbContext
    {
        public DbSet<Location> Locations { get; set; } = null!;
        public DbSet<Route> Routes { get; set; } = null!;
        public DbSet<Shipment> Shipments { get; set; } = null!;
        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<VehicleStatus> VehicleStatuses { get; set; } = null!;
        public DbSet<VehicleType> VehicleTypes { get; set; } = null!;

        public SQLiteContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=RoutesData.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>().HasKey(u => u.Id);
            modelBuilder.Entity<Route>().HasKey(u => u.Id);
            modelBuilder.Entity<Shipment>().HasKey(u => u.Id);
            modelBuilder.Entity<Vehicle>().HasKey(u => u.Id);
            modelBuilder.Entity<VehicleStatus>().HasKey(u => u.Id);
            modelBuilder.Entity<VehicleType>().HasKey(u => u.Id);

            modelBuilder.Entity<Location>().HasMany(t => t.RoutesStart)
                .WithOne(g => g.StartLocation)
                .HasForeignKey(g => g.StartLocationId);

            modelBuilder.Entity<Location>().HasMany(t => t.RoutesEnd)
                .WithOne(g => g.FinishLocation)
                .HasForeignKey(g => g.FinishLocationId);

            modelBuilder.Entity<Location>().HasMany(t => t.ShipmentsOrigin)
                .WithOne(g => g.Origin)
                .HasForeignKey(g => g.OriginId);

            modelBuilder.Entity<Location>().HasMany(t => t.ShipmentsDestination)
                .WithOne(g => g.Destination)
                .HasForeignKey(g => g.DestinationId);

            modelBuilder.Entity<VehicleStatus>().HasMany(t => t.Vehicles)
                .WithOne(g => g.Status)
                .HasForeignKey(g => g.StatusId);

            modelBuilder.Entity<VehicleType>().HasMany(t => t.Vehicles)
                .WithOne(g => g.Type)
                .HasForeignKey(g => g.TypeId);
        }
    }
}
