using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models.Entities
{
    public class ShipmentStatus: ReactiveObject, IEquatable<ShipmentStatus?>, ICloneable<ShipmentStatus>, ICopyable<ShipmentStatus?>
    {
        int _id;
        string _name;

        public ShipmentStatus()
        {
        }

        public ShipmentStatus(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public int Id
        {
            get => _id;
        }

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public virtual ICollection<Shipment>? Shipments { get; set; }

        public ShipmentStatus Clone()
        {
            var clone = new ShipmentStatus();
            clone.Name = _name;
            return clone;
        }

        public void CopyFrom(ShipmentStatus? entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Name = entity.Name;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as ShipmentStatus);
        }

        public bool Equals(ShipmentStatus? other)
        {
            return other is not null &&
                   _id == other._id &&
                   _name == other._name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_id, _name);
        }

        public static bool operator ==(ShipmentStatus? left, ShipmentStatus? right)
        {
            return EqualityComparer<ShipmentStatus>.Default.Equals(left, right);
        }

        public static bool operator !=(ShipmentStatus? left, ShipmentStatus? right)
        {
            return !(left == right);
        }
    }
}
