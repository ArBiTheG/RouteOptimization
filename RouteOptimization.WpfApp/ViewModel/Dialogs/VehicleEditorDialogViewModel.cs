using RouteOptimization.WpfApp.Models;
using RouteOptimization.WpfApp.Repository;
using RouteOptimization.WpfApp.Repository.SQLite;
using RouteOptimization.WpfApp.Utilties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.ViewModel.Dialogs
{
    public class VehicleEditorDialogViewModel : ViewModelBase
    {
        IVehicleStatusesRepository _statusesRepository;
        IVehicleTypesRepository _typesRepository;

        IVehicle? _vehicle;
        ObservableCollection<VehicleStatus?>? _statuses;
        ObservableCollection<VehicleType?>? _types;

        public IVehicle? Vehicle 
        { 
            get => _vehicle;
            set
            {
                _vehicle = value;
                OnPropertyChanged(nameof(Vehicle));
            }
        }

        public ObservableCollection<VehicleStatus?>? Statuses
        {
            get => _statuses;
            set
            {
                _statuses = value;
                OnPropertyChanged(nameof(Statuses));
            }
        }

        public ObservableCollection<VehicleType?>? Types
        {
            get => _types;
            set
            {
                _types = value;
                OnPropertyChanged(nameof(Types));
            }
        }
        public RelayCommand LoadCommand { get; }

        public VehicleEditorDialogViewModel()
        {
            _statusesRepository = new SQLiteVehicleStatusesRepository();
            _typesRepository = new SQLiteVehicleTypesRepository();

            LoadCommand = new RelayCommand(ExecuteLoadCommand);
        }

        private async void ExecuteLoadCommand(object? obj)
        {
            Types = new ObservableCollection<VehicleType?>(await _typesRepository.GetAll());
            Statuses = new ObservableCollection<VehicleStatus?>(await _statusesRepository.GetAll());
        }
    }
}
