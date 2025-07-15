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
    public class Route : ReactiveObject, IEquatable<Route?>, ICloneable<Route>, ICopyable<Route?>
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

        public Route Clone()
        {
            var clone = new Route();
            clone.StartLocationId = StartLocationId;
            clone.FinishLocationId = FinishLocationId;
            clone.StartLocation = StartLocation;
            clone.FinishLocation = FinishLocation;
            clone.Distance = Distance;
            clone.Time = Time;

            return clone;
        }

        public void CopyFrom(Route? entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            StartLocationId = entity.StartLocationId;
            FinishLocationId = entity.FinishLocationId;
            StartLocation = entity.StartLocation;
            FinishLocation = entity.FinishLocation;
            Distance = entity.Distance;
            Time = entity.Time;
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
                   _finishLocationId == other._finishLocationId &&
                   _distance == other._distance &&
                   _time == other._time;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_id, _startLocationId, _finishLocationId, _distance, _time);
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
