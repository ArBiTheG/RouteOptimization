using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using System.Security.Cryptography;

namespace RouteOptimization.Models.Entities
{
    public class VehicleStatus : ReactiveObject, IEquatable<VehicleStatus?>, ICloneable<VehicleStatus>, ICopyable<VehicleStatus?>
    {
        int _id;
        string _name;

        public VehicleStatus()
        {
        }

        public VehicleStatus(int id, string name)
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

        public VehicleStatus Clone()
        {
            var clone = new VehicleStatus();
            clone.Name = _name;
            return clone;
        }

        public void CopyFrom(VehicleStatus? entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Name = entity.Name;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as VehicleStatus);
        }

        public bool Equals(VehicleStatus? other)
        {
            return other is not null &&
                   _id == other._id &&
                   _name == other._name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_id, _name);
        }

        public static bool operator ==(VehicleStatus? left, VehicleStatus? right)
        {
            return EqualityComparer<VehicleStatus>.Default.Equals(left, right);
        }

        public static bool operator !=(VehicleStatus? left, VehicleStatus? right)
        {
            return !(left == right);
        }

    }

    public static class VehicleStatusValue
    {
        public static VehicleStatus Idle => new(1, "Простаивает");
        public static VehicleStatus Moving => new(2, "В движении");
    }
}
