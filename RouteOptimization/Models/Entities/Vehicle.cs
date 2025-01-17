using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RouteOptimization.Models.Entities
{
    public class Vehicle : ReactiveObject, IVehicle, IEquatable<Vehicle?>
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
                   _statusId == other._statusId &&
                   EqualityComparer<VehicleType?>.Default.Equals(_type, other._type) &&
                   EqualityComparer<VehicleStatus?>.Default.Equals(_status, other._status);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_id, _licensePlate, _capacity, _typeId, _statusId, _type, _status);
        }

        public override string? ToString()
        {
            return LicensePlate;
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
