using RouteOptimization.WpfApp.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.Models
{
    public class Location: ILocation, INotifyPropertyChanged
    {
        int _id;
        string? _name;
        string? _description;
        double _x;
        double _y;

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
        public double X 
        {
            get => _x;
            set
            {
                _x = value;
                OnPropertyChanged(nameof(X));
            }
        }
        public double Y 
        {
            get => _y;
            set
            {
                _y = value;
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
