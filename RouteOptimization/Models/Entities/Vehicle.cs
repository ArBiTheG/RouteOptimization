using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RouteOptimization.Models.Entities
{
    public class Vehicle : ReactiveObject
    {
        int _id;
        string _licensePlate;
        double _capacity;
        int _typeId;
        int _statusId;
        VehicleType? _type;
        VehicleStatus? _status;

        public int Id
        {
            get => _id;
        }
        public double Capacity
        {
            get => _capacity;
            set => this.RaiseAndSetIfChanged(ref _capacity, value);
        }
        public string LicensePlate
        {
            get => _licensePlate;
            set => this.RaiseAndSetIfChanged(ref _licensePlate, value);
        }

        public int TypeId
        {
            get => _typeId;
            set => this.RaiseAndSetIfChanged(ref _typeId, value);
        }

        public virtual VehicleType? Type
        {
            get => _type;
            set => this.RaiseAndSetIfChanged(ref _type, value);
        }

        public int StatusId
        {
            get => _statusId;
            set => this.RaiseAndSetIfChanged(ref _statusId, value);
        }

        public virtual VehicleStatus? Status
        {
            get => _status;
            set => this.RaiseAndSetIfChanged(ref _status, value);
        }

        public virtual ICollection<Shipment>? Shipments { get; set; }

        [NotMapped]
        public string Name => _type != null ? LicensePlate + " - " + _type.Name : LicensePlate;
    }
}
