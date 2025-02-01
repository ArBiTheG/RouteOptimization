using Avalonia.Animation;
using Microsoft.EntityFrameworkCore;
using RouteOptimization.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository.SQLite
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
        public async Task CreateShipmentsEditCargosVehicle(IEnumerable<Shipment> shipments, IEnumerable<Cargo> cargos, Vehicle vehicle)
        {
            using SQLiteContext context = new SQLiteContext();
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                foreach (var shipment in shipments)
                {
                    context.Shipments.Add(shipment);
                }
                foreach (var cargo in cargos)
                {
                    context.Cargos.Update(cargo);
                }
                context.Vehicles.Update(vehicle);

                await context.SaveChangesAsync();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
        }

        public async Task Delete(Shipment entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Shipments.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Update(Shipment entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Shipments.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Shipment?>> GetAll()
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Shipments.Include(t => t.Cargo).LoadAsync();
            return context.Shipments.Local.ToArray();
        }

        public async Task<IEnumerable<Shipment?>> GetAll(int page, int pageSize = 10, string filter = "")
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Shipments.Include(t=> t.Cargo).Skip((page - 1) * pageSize).Take(pageSize).LoadAsync();
            return context.Shipments.ToArray();
        }

        public async Task<Shipment?> GetByID(int id)
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.Shipments.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int?> Count()
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.Shipments.CountAsync();
        }

        public async Task AcceptShipment(Shipment shipment)
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Shipments.Include(t => t.Cargo).Include(t => t.Vehicle).LoadAsync();
            Shipment[] array = await context.Shipments.ToArrayAsync();
            
            Shipment? findedShipment = array.FirstOrDefault(array => array.Id == shipment.Id);

            if (findedShipment != null)
            {
                var cargo = findedShipment.Cargo;
                var vehicle = findedShipment.Vehicle;
                if (cargo != null && vehicle != null)
                {
                    findedShipment.StatusId = ShipmentStatusValue.Delivered.Id;

                    cargo.AvailableId = CargoAvailableValue.Present.Id;
                    cargo.LocationId = findedShipment.DestinationId;

                    vehicle.StatusId = VehicleStatusValue.Idle.Id;

                    context.Shipments.Update(findedShipment);
                    context.Cargos.Update(cargo);
                    context.Vehicles.Update(vehicle);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task CancelShipment(Shipment shipment)
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Shipments.Include(t => t.Cargo).Include(t => t.Vehicle).LoadAsync();
            Shipment[] array = await context.Shipments.ToArrayAsync();

            Shipment? findedShipment = array.FirstOrDefault(array => array.Id == shipment.Id);

            if (findedShipment != null)
            {
                var cargo = findedShipment.Cargo;
                var vehicle = findedShipment.Vehicle;
                if (cargo != null && vehicle != null)
                {
                    findedShipment.StatusId = ShipmentStatusValue.Cancelled.Id;

                    cargo.AvailableId = CargoAvailableValue.Present.Id;

                    vehicle.StatusId = VehicleStatusValue.Idle.Id;

                    context.Shipments.Update(findedShipment);
                    context.Cargos.Update(cargo);
                    context.Vehicles.Update(vehicle);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<IEnumerable<Shipment?>> GetUncompleteShipments()
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Shipments.Include(t => t.Cargo).Include(t => t.Vehicle).Where(t => t.StatusId == ShipmentStatusValue.Moving.Id).LoadAsync();
            return context.Shipments.Local.ToArray();
        }
    }
}
