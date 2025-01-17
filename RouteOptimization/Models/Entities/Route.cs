using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models.Entities
{
    public class Route : ReactiveObject, IRoute, IEquatable<Route?>
    {
        int _id;
        int _startLocationId;
        Location? _startLocation;
        int _finishLocationId;
        Location? _finishLocation;
        float _distance;
        float _time;

        public int Id
        {
            get => _id;
        }

        public int StartLocationId
        {
            get => _startLocationId;
            set => this.RaiseAndSetIfChanged(ref _startLocationId, value);
        }

        public virtual Location? StartLocation
        {
            get => _startLocation;
            set => this.RaiseAndSetIfChanged(ref _startLocation, value);
        }

        public int FinishLocationId
        {
            get => _finishLocationId;
            set => this.RaiseAndSetIfChanged(ref _finishLocationId, value);
        }

        public virtual Location? FinishLocation
        {
            get => _finishLocation;
            set => this.RaiseAndSetIfChanged(ref _finishLocation, value);
        }
        public float Distance
        {
            get => _distance;
            set => this.RaiseAndSetIfChanged(ref _distance, value);
        }
        public float Time
        {
            get => _time;
            set => this.RaiseAndSetIfChanged(ref _time, value);
        }

        public string Name
        {
            get
            {
                if (StartLocation != null && FinishLocation != null)
                {
                    return $"Маршрут {StartLocation.Name} - {FinishLocation.Name} ({Distance} м.)";
                }
                return $"Маршрут {Id}";
            }
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Route);
        }

        public bool Equals(Route? other)
        {
            return other is not null &&
                   _id == other._id &&
                   _startLocationId == other._startLocationId &&
                   EqualityComparer<Location?>.Default.Equals(_startLocation, other._startLocation) &&
                   _finishLocationId == other._finishLocationId &&
                   EqualityComparer<Location?>.Default.Equals(_finishLocation, other._finishLocation) &&
                   _distance == other._distance &&
                   _time == other._time;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_id, _startLocationId, _startLocation, _finishLocationId, _finishLocation, _distance, _time);
        }

        public override string? ToString()
        {
            return Name;
        }

        public static bool operator ==(Route? left, Route? right)
        {
            return EqualityComparer<Route>.Default.Equals(left, right);
        }

        public static bool operator !=(Route? left, Route? right)
        {
            return !(left == right);
        }
    }
}
