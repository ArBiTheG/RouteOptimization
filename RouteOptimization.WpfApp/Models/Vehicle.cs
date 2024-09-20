using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.Models
{
    public class Vehicle: IVehicle, INotifyPropertyChanged
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
            set
            {
                _capacity = value;
                OnPropertyChanged(nameof(Capacity));
            }
        }
        public string LicensePlate
        {
            get => _licensePlate;
            set
            {
                _licensePlate = value;
                OnPropertyChanged(nameof(LicensePlate));
            }
        }

        public int TypeId
        {
            get => _typeId;
            set
            {
                _typeId = value;
                OnPropertyChanged(nameof(TypeId));
            }
        }

        public virtual VehicleType? Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        public int StatusId
        {
            get => _statusId;
            set
            {
                _statusId = value;
                OnPropertyChanged(nameof(StatusId));
            }
        }

        public virtual VehicleStatus? Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
