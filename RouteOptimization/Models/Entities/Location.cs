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
    public class Location : ReactiveObject, ILocation, IEquatable<Location?>
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

        [NotMapped]
        public List<Route>? RoutesStart { get; set; }
        [NotMapped]
        public List<Route>? RoutesFinish { get; set; }

        [NotMapped]
        public List<Shipment>? ShipmentsOrigin { get; set; }
        [NotMapped]
        public List<Shipment>? ShipmentsDestination { get; set; }

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

        public override string? ToString()
        {
            return Name;
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
