using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models.Entities
{
    public class Cargo : ReactiveObject
    {
        private int _id;
        private string _name;
        private string? _description;
        private double _weight;
        private double _sizeX;
        private double _sizeY;
        private double _sizeZ;
        private int _availableId;
        private CargoAvailable? _available;
        private int _locationId;
        private Location? _location;

        public int Id => _id;
        public string Name { get => _name; set => this.RaiseAndSetIfChanged(ref _name, value); }
        public string? Description { get => _description; set => this.RaiseAndSetIfChanged(ref _description, value); }
        public double Weight { get => _weight; set => this.RaiseAndSetIfChanged(ref _weight, value); }
        public double SizeX { get => _sizeX; set => this.RaiseAndSetIfChanged(ref _sizeX, value); }
        public double SizeY { get => _sizeY; set => this.RaiseAndSetIfChanged(ref _sizeY, value); }
        public double SizeZ { get => _sizeZ; set => this.RaiseAndSetIfChanged(ref _sizeZ, value); }
        public int AvailableId { get => _availableId; set => this.RaiseAndSetIfChanged(ref _availableId, value); }
        public virtual CargoAvailable? Available { get => _available; set => this.RaiseAndSetIfChanged(ref _available, value); }
        public int LocationId { get => _locationId; set => this.RaiseAndSetIfChanged(ref _locationId, value); }
        public virtual Location? Location { get => _location; set => this.RaiseAndSetIfChanged(ref _location, value); }

        public virtual ICollection<Shipment>? Shipments { get; set; }
    }
}
