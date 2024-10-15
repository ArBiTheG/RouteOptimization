using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models
{
    public class Shipment : IShipment, INotifyPropertyChanged
    {
        int _id;
        string _name;
        double _weight;
        DateTime _dateTime;
        int _originId;
        Location? _origin;
        int _destinationId;
        Location? _destination;

        public int Id
        {
            get => _id;
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public double Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }
        public DateTime DateTime
        {
            get => _dateTime;
            set
            {
                _dateTime = value;
                OnPropertyChanged(nameof(DateTime));
            }
        }

        public int OriginId
        {
            get => _originId;
            set
            {
                _originId = value;
                OnPropertyChanged(nameof(OriginId));
            }
        }

        public virtual Location? Origin
        {
            get => _origin;
            set
            {
                _origin = value;
                OnPropertyChanged(nameof(Origin));
            }
        }

        public int DestinationId
        {
            get => _destinationId;
            set
            {
                _destinationId = value;
                OnPropertyChanged(nameof(DestinationId));
            }
        }

        public virtual Location? Destination
        {
            get => _destination;
            set
            {
                _destination = value;
                OnPropertyChanged(nameof(Destination));
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
