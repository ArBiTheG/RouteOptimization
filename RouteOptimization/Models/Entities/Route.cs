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
    public class Route : ReactiveObject, IRoute
    {
        int _id;
        int _startLocationId;
        Location? _startLocation;
        int _endLocationId;
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
            get => _endLocationId;
            set => this.RaiseAndSetIfChanged(ref _endLocationId, value);
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
    }
}
