using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models.Entities
{
    public class CargoAvailable: ReactiveObject
    {
        private int _id;
        private string _name;

        public CargoAvailable(){}

        public CargoAvailable(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public int Id => _id;
        public string Name { get => _name; set => this.RaiseAndSetIfChanged(ref _name, value); }

        public virtual ICollection<Cargo>? Cargos { get; set; }

    }
}
