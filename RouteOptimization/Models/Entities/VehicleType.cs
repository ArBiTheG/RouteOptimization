using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace RouteOptimization.Models.Entities
{
    public class VehicleType : ReactiveObject, IEquatable<VehicleType?>, ICloneable<VehicleType>, ICopyable<VehicleType?>
    {
        int _id;
        string _name;

        public VehicleType()
        {
        }

        public VehicleType(int id, string name)
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

        public virtual ICollection<Vehicle>? Vehicles { get; set; }

        public VehicleType Clone()
        {
            var clone = new VehicleType();
            clone.Name = _name;
            return clone;
        }

        public void CopyFrom(VehicleType? entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Name = entity.Name;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as VehicleType);
        }

        public bool Equals(VehicleType? other)
        {
            return other is not null &&
                   _id == other._id &&
                   _name == other._name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_id, _name);
        }

        public static bool operator ==(VehicleType? left, VehicleType? right)
        {
            return EqualityComparer<VehicleType>.Default.Equals(left, right);
        }

        public static bool operator !=(VehicleType? left, VehicleType? right)
        {
            return !(left == right);
        }
    }
}
