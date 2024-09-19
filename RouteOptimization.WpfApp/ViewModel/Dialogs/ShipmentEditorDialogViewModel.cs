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
using System.Windows.Documents;

namespace RouteOptimization.WpfApp.ViewModel.Dialogs
{
    public class ShipmentEditorDialogViewModel : ViewModelBase
    {
        ILocationsRepository _locationRepository;

        IShipment? _shipment;
        ObservableCollection<Location?>? _locations;

        public IShipment? Shipment 
        { 
            get => _shipment; 
            set
            {
                _shipment = value;
                OnPropertyChanged(nameof(Shipment));
            } 
        }

        public ObservableCollection<Location?>? Locations 
        { 
            get => _locations;
            set
            {
                _locations = value;
                OnPropertyChanged(nameof(Locations));
            } 
        }

        public RelayCommand LoadCommand { get; }
        public ShipmentEditorDialogViewModel()
        {
            _locationRepository = new SQLiteLocationsRepository();

            LoadCommand = new RelayCommand(ExecuteLoadCommand);
        }

        private async void ExecuteLoadCommand(object? obj)
        {
            Locations = new(await _locationRepository.GetAll());
        }
    }
}
