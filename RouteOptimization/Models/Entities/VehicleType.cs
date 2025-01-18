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
    public class VehicleType : ReactiveObject
    {
        int _id;
        string _name;

        public VehicleType()
        {
        }

        public VehicleType(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public int Id
        {
            get => _id;
        }

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public virtual ICollection<Vehicle>? Vehicles { get; set; }
    }
}
