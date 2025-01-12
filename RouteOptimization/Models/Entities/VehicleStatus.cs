using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace RouteOptimization.Models.Entities
{
    public class VehicleStatus : ReactiveObject, IVehicleStatus
    {
        int _id;
        string _name;

        public int Id
        {
            get => _id;
        }

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        [NotMapped]
        public List<Vehicle>? Vehicles { get; set; }

        public override string? ToString()
        {
            return Name;
        }
    }
}
