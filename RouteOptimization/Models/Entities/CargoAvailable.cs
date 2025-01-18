using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models.Entities
{
    public class CargoAvailable: ReactiveObject, IEquatable<CargoAvailable?>, ICloneable<CargoAvailable>, ICopyable<CargoAvailable?>
    {
        private int _id;
        private string _name;

        public CargoAvailable(){}

        public CargoAvailable(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public int Id => _id;
        public string Name { get => _name; set => this.RaiseAndSetIfChanged(ref _name, value); }

        public virtual ICollection<Cargo>? Cargos { get; set; }

        public CargoAvailable Clone()
        {
            var clone = new CargoAvailable();
            clone.Name = _name;
            return clone;
        }

        public void CopyFrom(CargoAvailable? entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Name = entity.Name;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as CargoAvailable);
        }

        public bool Equals(CargoAvailable? other)
        {
            return other is not null &&
                   _id == other._id &&
                   _name == other._name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_id, _name);
        }

        public static bool operator ==(CargoAvailable? left, CargoAvailable? right)
        {
            return EqualityComparer<CargoAvailable>.Default.Equals(left, right);
        }

        public static bool operator !=(CargoAvailable? left, CargoAvailable? right)
        {
            return !(left == right);
        }
    }
}
