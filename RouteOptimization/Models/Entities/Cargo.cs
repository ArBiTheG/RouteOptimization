using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models.Entities
{
    public class Cargo : ReactiveObject, IEquatable<Cargo?>, ICloneable<Cargo>, ICopyable<Cargo?>
    {
        private int _id;
        private string _name;
        private string? _description;
        private double _weight;
        private double _sizeX;
        private double _sizeY;
        private double _sizeZ;
        private int _availableId;
        private CargoAvailable? _available;
        private int _locationId;
        private Location? _location;

        public int Id => _id;
        public string Name { get => _name; set => this.RaiseAndSetIfChanged(ref _name, value); }
        public string? Description { get => _description; set => this.RaiseAndSetIfChanged(ref _description, value); }
        public double Weight { get => _weight; set => this.RaiseAndSetIfChanged(ref _weight, value); }
        public double SizeX { get => _sizeX; set => this.RaiseAndSetIfChanged(ref _sizeX, value); }
        public double SizeY { get => _sizeY; set => this.RaiseAndSetIfChanged(ref _sizeY, value); }
        public double SizeZ { get => _sizeZ; set => this.RaiseAndSetIfChanged(ref _sizeZ, value); }
        public int AvailableId { get => _availableId; set => this.RaiseAndSetIfChanged(ref _availableId, value); }
        public virtual CargoAvailable? Available { get => _available; set => this.RaiseAndSetIfChanged(ref _available, value); }
        public int LocationId { get => _locationId; set => this.RaiseAndSetIfChanged(ref _locationId, value); }
        public virtual Location? Location { get => _location; set => this.RaiseAndSetIfChanged(ref _location, value); }

        public virtual ICollection<Shipment>? Shipments { get; set; }

        public Cargo Clone()
        {
            var clone = new Cargo();
            clone.Name = _name;
            clone.Description = _description;
            clone.Weight = _weight;
            clone.SizeX = _sizeX;
            clone.SizeY = _sizeY;
            clone.SizeZ = _sizeZ;
            clone.AvailableId = _availableId;
            clone.Available = _available;
            clone.LocationId = _locationId;
            clone.Location = _location;
            return clone;
        }

        public void CopyFrom(Cargo? entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Name = entity.Name;
            Description = entity.Description;
            Weight = entity.Weight;
            SizeX = entity.SizeX;
            SizeY = entity.SizeY;
            SizeZ = entity.SizeZ;
            AvailableId = entity.AvailableId;
            Available = entity.Available;
            LocationId = entity.LocationId;
            Location = entity.Location;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Cargo);
        }

        public bool Equals(Cargo? other)
        {
            return other is not null &&
                   _id == other._id &&
                   _name == other._name &&
                   _description == other._description &&
                   _weight == other._weight &&
                   _sizeX == other._sizeX &&
                   _sizeY == other._sizeY &&
                   _sizeZ == other._sizeZ &&
                   _availableId == other._availableId &&
                   _locationId == other._locationId;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(_id);
            hash.Add(_name);
            hash.Add(_description);
            hash.Add(_weight);
            hash.Add(_sizeX);
            hash.Add(_sizeY);
            hash.Add(_sizeZ);
            hash.Add(_availableId);
            hash.Add(_locationId);
            return hash.ToHashCode();
        }

        public static bool operator ==(Cargo? left, Cargo? right)
        {
            return EqualityComparer<Cargo>.Default.Equals(left, right);
        }

        public static bool operator !=(Cargo? left, Cargo? right)
        {
            return !(left == right);
        }
    }
}
