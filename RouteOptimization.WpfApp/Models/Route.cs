using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.Models
{
    public class Route: IRoute, INotifyPropertyChanged
    {
        int _id;
        int _startLocationId;
        Location? _startLocation;
        int _endLocationId;
        Location? _endLocation;
        double _distance;
        double _time;

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

        public virtual Location? StartLocation
        {
            get => _startLocation;
            set
            {
                _startLocation = value;
                OnPropertyChanged(nameof(StartLocation));
            }
        }

        public int EndLocationId
        {
            get => _endLocationId;
            set
            {
                _endLocationId = value;
                OnPropertyChanged(nameof(EndLocationId));
            }
        }

        public virtual Location? EndLocation
        {
            get => _endLocation;
            set
            {
                _endLocation = value;
                OnPropertyChanged(nameof(EndLocation));
            }
        }
        public double Distance
        {
            get => _distance;
            set
            {
                _distance = value;
                OnPropertyChanged(nameof(Distance));
            }
        }
        public double Time
        {
            get => _time;
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
