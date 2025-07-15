using Avalonia;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RouteOptimization.Models.Entities
{
    public class Vehicle : ReactiveObject, IEquatable<Vehicle?>, ICloneable<Vehicle>, ICopyable<Vehicle?>
    {
        int _id;
        string _licensePlate;
        double _capacity;
        int _typeId;
        int _statusId;
        VehicleType? _type;
        VehicleStatus? _status;

        public int Id
        {
            get => _id;
        }
        public double Capacity
        {
            get => _capacity;
            set => this.RaiseAndSetIfChanged(ref _capacity, value);
        }
        public string LicensePlate
        {
            get => _licensePlate;
            set => this.RaiseAndSetIfChanged(ref _licensePlate, value);
        }

        public int TypeId
        {
            get => _typeId;
            set => this.RaiseAndSetIfChanged(ref _typeId, value);
        }

        public virtual VehicleType? Type
        {
            get => _type;
            set => this.RaiseAndSetIfChanged(ref _type, value);
        }

        public int StatusId
        {
            get => _statusId;
            set => this.RaiseAndSetIfChanged(ref _statusId, value);
        }

        public virtual VehicleStatus? Status
        {
            get => _status;
            set => this.RaiseAndSetIfChanged(ref _status, value);
        }

        public virtual ICollection<Shipment>? Shipments { get; set; }

        [NotMapped]
        public string Name => _type != null ? LicensePlate + " - " + _type.Name : LicensePlate;

        public Vehicle Clone()
        {
            var clone = new Vehicle();
            clone.LicensePlate = LicensePlate;
            clone.Capacity = Capacity;
            clone.TypeId = TypeId;
            clone.Type = Type;
            clone.StatusId = StatusId;
            clone.Status = Status;

            return clone;
        }

        public void CopyFrom(Vehicle? entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            LicensePlate = entity.LicensePlate;
            Capacity = entity.Capacity;
            TypeId = entity.TypeId;
            Type = entity.Type;
            StatusId = entity.StatusId;
            Status = entity.Status;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Vehicle);
        }

        public bool Equals(Vehicle? other)
        {
            return other is not null &&
                   _id == other._id &&
                   _licensePlate == other._licensePlate &&
                   _capacity == other._capacity &&
                   _typeId == other._typeId &&
                   _statusId == other._statusId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_id, _licensePlate, _capacity, _typeId, _statusId);
        }

        public static bool operator ==(Vehicle? left, Vehicle? right)
        {
            return EqualityComparer<Vehicle>.Default.Equals(left, right);
        }

        public static bool operator !=(Vehicle? left, Vehicle? right)
        {
            return !(left == right);
        }
    }
}
