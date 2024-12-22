using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RouteOptimization.Models
{
    public class Location : ILocation, INotifyPropertyChanged
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
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string? Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public float X
        {
            get => _x;
            set
            {
                _x = value;
                OnPropertyChanged(nameof(X));
            }
        }
        public float Y
        {
            get => _y;
            set
            {
                _y = value;
                OnPropertyChanged(nameof(Y));
            }
        }
        public float Size
        {
            get => _size;
            set
            {
                _size = value;
                OnPropertyChanged(nameof(Y));
            }
        }

        [NotMapped]
        public List<Route>? RoutesStart { get; set; }
        [NotMapped]
        public List<Route>? RoutesEnd { get; set; }

        [NotMapped]
        public List<Shipment>? ShipmentsOrigin { get; set; }
        [NotMapped]
        public List<Shipment>? ShipmentsDestination { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
