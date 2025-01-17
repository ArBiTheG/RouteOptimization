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
    public class VehicleType : ReactiveObject, IVehicleType, IEquatable<VehicleType?>
    {
        int _id;
        string _name;

        public int Id
        {
            get => _id;
        }

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        [NotMapped]
        public List<Vehicle>? Vehicles { get; set; }

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

        public override string? ToString()
        {
            return Name;
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
