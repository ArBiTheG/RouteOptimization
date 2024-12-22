using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models
{
    public class Route : IRoute, INotifyPropertyChanged
    {
        int _id;
        int _startLocationId;
        Location _startLocation;
        int _endLocationId;
        Location _finishLocation;
        float _distance;
        float _time;

        public int Id
        {
            get => _id;
        }

        public int StartLocationId
        {
            get => _startLocationId;
            set
            {
                _startLocationId = value;
                OnPropertyChanged(nameof(StartLocationId));
            }
        }

        public virtual Location StartLocation
        {
            get => _startLocation;
            set
            {
                _startLocation = value;
                OnPropertyChanged(nameof(StartLocation));
            }
        }

        public int FinishLocationId
        {
            get => _endLocationId;
            set
            {
                _endLocationId = value;
                OnPropertyChanged(nameof(FinishLocationId));
            }
        }

        public virtual Location FinishLocation
        {
            get => _finishLocation;
            set
            {
                _finishLocation = value;
                OnPropertyChanged(nameof(FinishLocation));
            }
        }
        public float Distance
        {
            get => _distance;
            set
            {
                _distance = value;
                OnPropertyChanged(nameof(Distance));
            }
        }
        public float Time
        {
            get => _time;
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
            }
        }

        [NotMapped]
        public float StartX { get => StartLocation.X; set => StartLocation.X = value; }

        [NotMapped]
        public float StartY { get => StartLocation.Y; set => StartLocation.Y = value; }

        [NotMapped]
        public float FinishX { get => FinishLocation.X; set => FinishLocation.X = value; }

        [NotMapped]
        public float FinishY { get => FinishLocation.Y; set => FinishLocation.Y = value; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
