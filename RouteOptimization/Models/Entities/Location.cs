using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ReactiveUI;

namespace RouteOptimization.Models.Entities
{
    public class Location : ReactiveObject, ILocation
    {
        private int _id;
        private string? _name;
        private string? _description;
        private float _x;
        private float _y;
        private float _size = 20;

        public int Id
        {
            get => _id;
        }
        public string? Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
        public string? Description
        {
            get => _description;
            set => this.RaiseAndSetIfChanged(ref _description, value);
        }
        public float X
        {
            get => _x;
            set => this.RaiseAndSetIfChanged(ref _x, value);
        }
        public float Y
        {
            get => _y;
            set => this.RaiseAndSetIfChanged(ref _y, value);
        }
        public float Size
        {
            get => _size;
            set => this.RaiseAndSetIfChanged(ref _size, value);
        }
        public virtual ICollection<Cargo>? Cargos { get; set; }

        public virtual ICollection<Route>? RoutesStart { get; set; }
        public virtual ICollection<Route>? RoutesFinish { get; set; }

        public virtual ICollection<Shipment>? ShipmentsOrigin { get; set; }
        public virtual ICollection<Shipment>? ShipmentsDestination { get; set; }

    }
}
