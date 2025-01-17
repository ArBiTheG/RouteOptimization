using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models.Entities
{
    public class Shipment : ReactiveObject, IShipment, IEquatable<Shipment?>
    {
        int _id;
        string _name;
        double _weight;
        DateTime _dateTime;
        int _originId;
        Location? _origin;
        int _destinationId;
        Location? _destination;

        public int Id
        {
            get => _id;
        }
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
        public double Weight
        {
            get => _weight;
            set => this.RaiseAndSetIfChanged(ref _weight, value);
        }
        public DateTime DateTime
        {
            get => _dateTime;
            set => this.RaiseAndSetIfChanged(ref _dateTime, value);
        }

        public int OriginId
        {
            get => _originId;
            set => this.RaiseAndSetIfChanged(ref _originId, value);
        }

        public virtual Location? Origin
        {
            get => _origin;
            set => this.RaiseAndSetIfChanged(ref _origin, value);
        }

        public int DestinationId
        {
            get => _destinationId;
            set => this.RaiseAndSetIfChanged(ref _destinationId, value);
        }

        public virtual Location? Destination
        {
            get => _destination;
            set => this.RaiseAndSetIfChanged(ref _destination, value);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Shipment);
        }

        public bool Equals(Shipment? other)
        {
            return other is not null &&
                   _id == other._id &&
                   _name == other._name &&
                   _weight == other._weight &&
                   _dateTime == other._dateTime &&
                   _originId == other._originId &&
                   EqualityComparer<Location?>.Default.Equals(_origin, other._origin) &&
                   _destinationId == other._destinationId &&
                   EqualityComparer<Location?>.Default.Equals(_destination, other._destination);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_id, _name, _weight, _dateTime, _originId, _origin, _destinationId, _destination);
        }

        public override string? ToString()
        {
            return Name;
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
