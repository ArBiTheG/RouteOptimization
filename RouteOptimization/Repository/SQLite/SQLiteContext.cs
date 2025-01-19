using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RouteOptimization.Models.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public DbSet<Cargo> Cargos { get; set; } = null!;
        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<CargoAvailable> CargoAvailables { get; set; } = null!;
        public DbSet<ShipmentStatus> ShipmentStatuses { get; set; } = null!;
        public DbSet<VehicleStatus> VehicleStatuses { get; set; } = null!;
        public DbSet<VehicleType> VehicleTypes { get; set; } = null!;

        public SQLiteContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=RoutesData.db");
            optionsBuilder.LogTo(SendMessage);
        }

        private void SendMessage(string message)
        {
            Trace.WriteLine("SQLiteDataContext:");
            Trace.WriteLine(message);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Location
            modelBuilder.Entity<Location>().HasKey(u => u.Id);

            modelBuilder.Entity<Location>().HasMany(u => u.RoutesStart).WithOne(u => u.StartLocation).HasForeignKey(u => u.StartLocationId);
            modelBuilder.Entity<Location>().HasMany(u => u.RoutesFinish).WithOne(u => u.FinishLocation).HasForeignKey(u => u.FinishLocationId);

            modelBuilder.Entity<Location>().HasMany(u => u.ShipmentsOrigin).WithOne(u => u.Origin).HasForeignKey(u => u.OriginId);
            modelBuilder.Entity<Location>().HasMany(u => u.ShipmentsDestination).WithOne(u => u.Destination).HasForeignKey(u => u.DestinationId);

            modelBuilder.Entity<Location>().HasMany(u => u.Cargos).WithOne(u => u.Location).HasForeignKey(u => u.LocationId);

            // Route
            modelBuilder.Entity<Route>().HasKey(u => u.Id);

            // Shipment
            modelBuilder.Entity<Shipment>().HasKey(u => u.Id);

            // Cargo
            modelBuilder.Entity<Cargo>().HasKey(u => u.Id);
            modelBuilder.Entity<Cargo>().HasMany(u => u.Shipments).WithOne(u => u.Cargo).HasForeignKey(u => u.CargoId);

            // Vehicle
            modelBuilder.Entity<Vehicle>().HasKey(u => u.Id);
            modelBuilder.Entity<Vehicle>().HasMany(u => u.Shipments).WithOne(u => u.Vehicle).HasForeignKey(u => u.VehicleId);

            // CargoAvailable
            modelBuilder.Entity<CargoAvailable>().HasKey(u => u.Id);
            modelBuilder.Entity<CargoAvailable>().HasMany(u => u.Cargos).WithOne(u => u.Available).HasForeignKey(u => u.AvailableId);

            // ShipmentStatus
            modelBuilder.Entity<ShipmentStatus>().HasKey(u => u.Id);
            modelBuilder.Entity<ShipmentStatus>().HasMany(u => u.Shipments).WithOne(u => u.Status).HasForeignKey(u => u.StatusId);

            // VehicleStatus
            modelBuilder.Entity<VehicleStatus>().HasKey(u => u.Id);
            modelBuilder.Entity<VehicleStatus>().HasMany(u => u.Vehicles).WithOne(u => u.Status).HasForeignKey(u => u.StatusId);

            // VehicleType
            modelBuilder.Entity<VehicleType>().HasKey(u => u.Id);
            modelBuilder.Entity<VehicleType>().HasMany(u => u.Vehicles).WithOne(u => u.Type).HasForeignKey(u => u.TypeId);

            modelBuilder.Entity<CargoAvailable>().HasData(
                 new CargoAvailable(1,"Отсутствует"),
                 new CargoAvailable(2,"В наличии"),
                 new CargoAvailable(3,"В пути"),
                 new CargoAvailable(4,"Ожидается"),
                 new CargoAvailable(5,"Продано")
                );

            modelBuilder.Entity<ShipmentStatus>().HasData(
                 new ShipmentStatus(1,"Не доставлено"),
                 new ShipmentStatus(2,"Доставляется"),
                 new ShipmentStatus(3,"Доставлено")
                );

            modelBuilder.Entity<VehicleStatus>().HasData(
                 new VehicleStatus(1,"Не зайдествован"),
                 new VehicleStatus(2,"Занят"),
                 new VehicleStatus(3,"Неисправно")
                );

            modelBuilder.Entity<VehicleType>().HasData(
                 new VehicleType(1,"Бортовой грузовик"),
                 new VehicleType(2,"Фургон"),
                 new VehicleType(3,"Тягач"),
                 new VehicleType(4,"Платформа"),
                 new VehicleType(5,"Цистерна")
                );



        }
    }
}
