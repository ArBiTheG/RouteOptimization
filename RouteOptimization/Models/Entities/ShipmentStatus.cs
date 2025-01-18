using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models.Entities
{
    public class ShipmentStatus: ReactiveObject
    {
        int _id;
        string _name;

        public ShipmentStatus()
        {
        }

        public ShipmentStatus(int id, string name)
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

        public virtual ICollection<Shipment>? Shipments { get; set; }
    }
}
