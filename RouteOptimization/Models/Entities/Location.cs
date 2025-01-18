using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ReactiveUI;

namespace RouteOptimization.Models.Entities
{
    public class Location : ReactiveObject, ILocation, IEquatable<Location?>, ICloneable<Location>, ICopyable<Location?>
    {
        private int _id;
        private string? _name;
        private string? _description;
        private float _x;
        private float _y;
        private float _size = 20;

        public int Id
        {
            get => _id;
        }
        public string? Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
        public string? Description
        {
            get => _description;
            set => this.RaiseAndSetIfChanged(ref _description, value);
        }
        public float X
        {
            get => _x;
            set => this.RaiseAndSetIfChanged(ref _x, value);
        }
        public float Y
        {
            get => _y;
            set => this.RaiseAndSetIfChanged(ref _y, value);
        }
        public float Size
        {
            get => _size;
            set => this.RaiseAndSetIfChanged(ref _size, value);
        }
        public virtual ICollection<Cargo>? Cargos { get; set; }

        public virtual ICollection<Route>? RoutesStart { get; set; }
        public virtual ICollection<Route>? RoutesFinish { get; set; }

        public virtual ICollection<Shipment>? ShipmentsOrigin { get; set; }
        public virtual ICollection<Shipment>? ShipmentsDestination { get; set; }

        public Location Clone()
        {
            var clone = new Location();
            clone.Name = Name;
            clone.Description = Description;
            clone.X = X;
            clone.Y = Y;
            clone.Size = Size;

            return clone;
        }

        public void CopyFrom(Location? entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Name = entity.Name;
            Description = entity.Description;
            X = entity.X;
            Y = entity.Y;
            Size = entity.Size;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Location);
        }

        public bool Equals(Location? other)
        {
            return other is not null &&
                   _id == other._id &&
                   _name == other._name &&
                   _description == other._description &&
                   _x == other._x &&
                   _y == other._y &&
                   _size == other._size;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_id, _name, _description, _x, _y, _size);
        }

        public static bool operator ==(Location? left, Location? right)
        {
            return EqualityComparer<Location>.Default.Equals(left, right);
        }

        public static bool operator !=(Location? left, Location? right)
        {
            return !(left == right);
        }
    }
}
