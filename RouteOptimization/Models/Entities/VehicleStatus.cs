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
    public class VehicleStatus : ReactiveObject, IVehicleStatus, IEquatable<VehicleStatus?>
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
            return Equals(obj as VehicleStatus);
        }

        public bool Equals(VehicleStatus? other)
        {
            return other is not null &&
                   _name == other._name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_name);
        }

        public override string? ToString()
        {
            return Name;
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
}
