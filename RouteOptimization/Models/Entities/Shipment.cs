using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models.Entities
{
    public class Shipment : ReactiveObject, IEquatable<Shipment?>, ICloneable<Shipment>, ICopyable<Shipment?>
    {
        private int _id;
        private int _vehicleId;
        private Vehicle? _vehicle;
        private int _cargoId;
        private Cargo? _cargo;
        private DateTime _dateTimeStart;
        private DateTime _dateTimeFinish;
        private int _locationOriginId;
        private Location? _locationOrigin;
        private int _locationDestinationId;
        private Location? _locationDestination;
        private int _shipmentStatusId;
        private ShipmentStatus? _shipmentStatus;

        public int Id => _id;
        public DateTime DateTimeStart { get => _dateTimeStart; set => this.RaiseAndSetIfChanged(ref _dateTimeStart, value); }
        public DateTime DateTimeFinish { get => _dateTimeFinish; set => this.RaiseAndSetIfChanged(ref _dateTimeFinish, value); }
        public int VehicleId { get => _vehicleId; set => this.RaiseAndSetIfChanged(ref _vehicleId, value); }
        public virtual Vehicle? Vehicle { get => _vehicle; set => this.RaiseAndSetIfChanged(ref _vehicle, value); }
        public int CargoId { get => _cargoId; set => this.RaiseAndSetIfChanged(ref _cargoId, value); }
        public virtual Cargo? Cargo { get => _cargo; set => this.RaiseAndSetIfChanged(ref _cargo, value); }
        public int OriginId { get => _locationOriginId; set => this.RaiseAndSetIfChanged(ref _locationOriginId, value); }
        public virtual Location? Origin { get => _locationOrigin; set => this.RaiseAndSetIfChanged(ref _locationOrigin, value); }
        public int DestinationId { get => _locationDestinationId; set => this.RaiseAndSetIfChanged(ref _locationDestinationId, value); }
        public virtual Location? Destination { get => _locationDestination; set => this.RaiseAndSetIfChanged(ref _locationDestination, value); }
        public int StatusId { get => _shipmentStatusId; set => this.RaiseAndSetIfChanged(ref _shipmentStatusId, value); }
        public virtual ShipmentStatus? Status { get => _shipmentStatus; set => this.RaiseAndSetIfChanged(ref _shipmentStatus, value); }

        [NotMapped]
        public string Name => _cargo != null ? "Доставка #" + Id +  " - " + _cargo.Name : "Доставка #" + Id;

        public Shipment Clone()
        {
            var clone = new Shipment();
            clone.VehicleId = VehicleId;
            clone.Vehicle = Vehicle;
            clone.CargoId = CargoId;
            clone.Cargo = Cargo;
            clone.DateTimeStart = DateTimeStart;
            clone.DateTimeFinish = DateTimeFinish;
            clone.OriginId = OriginId;
            clone.Origin = Origin;
            clone.DestinationId = DestinationId;
            clone.Destination = Destination;
            clone.StatusId = StatusId;
            clone.Status = Status;

            return clone;
        }

        public void CopyFrom(Shipment? entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            VehicleId = entity.VehicleId;
            Vehicle = entity.Vehicle;
            CargoId = entity.CargoId;
            Cargo = entity.Cargo;
            DateTimeStart = entity.DateTimeStart;
            DateTimeFinish = entity.DateTimeFinish;
            OriginId = entity.OriginId;
            Origin = entity.Origin;
            DestinationId = entity.DestinationId;
            Destination = entity.Destination;
            StatusId = entity.StatusId;
            Status = entity.Status;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Shipment);
        }

        public bool Equals(Shipment? other)
        {
            return other is not null &&
                   _id == other._id &&
                   _vehicleId == other._vehicleId &&
                   _cargoId == other._cargoId &&
                   _dateTimeStart == other._dateTimeStart &&
                   _dateTimeFinish == other._dateTimeFinish &&
                   _locationOriginId == other._locationOriginId &&
                   _locationDestinationId == other._locationDestinationId &&
                   _shipmentStatusId == other._shipmentStatusId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_id, _vehicleId, _cargoId, _dateTimeStart, _dateTimeFinish, _locationOriginId, _locationDestinationId, _shipmentStatusId);
        }

        public static bool operator ==(Shipment? left, Shipment? right)
        {
            return EqualityComparer<Shipment>.Default.Equals(left, right);
        }

        public static bool operator !=(Shipment? left, Shipment? right)
        {
            return !(left == right);
        }
    }
}
