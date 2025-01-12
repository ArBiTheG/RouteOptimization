using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models.Entities
{
    public class Shipment : ReactiveObject, IShipment
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
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
        public double Weight
        {
            get => _weight;
            set => this.RaiseAndSetIfChanged(ref _weight, value);
        }
        public DateTime DateTime
        {
            get => _dateTime;
            set => this.RaiseAndSetIfChanged(ref _dateTime, value);
        }

        public int OriginId
        {
            get => _originId;
            set => this.RaiseAndSetIfChanged(ref _originId, value);
        }

        public virtual Location? Origin
        {
            get => _origin;
            set => this.RaiseAndSetIfChanged(ref _origin, value);
        }

        public int DestinationId
        {
            get => _destinationId;
            set => this.RaiseAndSetIfChanged(ref _destinationId, value);
        }

        public virtual Location? Destination
        {
            get => _destination;
            set => this.RaiseAndSetIfChanged(ref _destination, value);
        }

        public override string? ToString()
        {
            return Name;
        }
    }
}
