using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models.Entities
{
    // TODO: Переделать
    public class Shipment : ReactiveObject
    {
        private int _id;
        private int _vehicleId;
        private Vehicle? _vehicle;
        private int _cargoId;
        private Cargo? _cargo;
        private DateTime _dateTimeStart;
        private DateTime _dateTimeFinish;
        private int _locationOriginId;
        private Location? _locationOrigin;
        private int _locationDestinationId;
        private Location? _locationDestination;
        private int _shipmentStatusId;
        private ShipmentStatus? _shipmentStatus;

        public int Id => _id;
        public DateTime DateTimeStart { get => _dateTimeStart; set => this.RaiseAndSetIfChanged(ref _dateTimeStart, value); }
        public DateTime DateTimeFinish { get => _dateTimeFinish; set => this.RaiseAndSetIfChanged(ref _dateTimeFinish, value); }
        public int VehicleId { get => _vehicleId; set => this.RaiseAndSetIfChanged(ref _vehicleId, value); }
        public virtual Vehicle? Vehicle { get => _vehicle; set => this.RaiseAndSetIfChanged(ref _vehicle, value); }
        public int CargoId { get => _cargoId; set => this.RaiseAndSetIfChanged(ref _cargoId, value); }
        public virtual Cargo? Cargo { get => _cargo; set => this.RaiseAndSetIfChanged(ref _cargo, value); }
        public int OriginId { get => _locationOriginId; set => this.RaiseAndSetIfChanged(ref _locationOriginId, value); }
        public virtual Location? Origin { get => _locationOrigin; set => this.RaiseAndSetIfChanged(ref _locationOrigin, value); }
        public int DestinationId { get => _locationDestinationId; set => this.RaiseAndSetIfChanged(ref _locationDestinationId, value); }
        public virtual Location? Destination { get => _locationDestination; set => this.RaiseAndSetIfChanged(ref _locationDestination, value); }
        public int StatusId { get => _shipmentStatusId; set => this.RaiseAndSetIfChanged(ref _shipmentStatusId, value); }
        public virtual ShipmentStatus? Status { get => _shipmentStatus; set => this.RaiseAndSetIfChanged(ref _shipmentStatus, value); }

        [NotMapped]
        public string Name => _cargo != null ? "Доставка #" + Id +  " - " + _cargo.Name : "Доставка #" + Id;
    }
}
