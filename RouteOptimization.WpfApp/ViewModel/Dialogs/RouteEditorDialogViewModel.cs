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
    public class RouteEditorDialogViewModel : ViewModelBase
    {
        ILocationsRepository _locationRepository;

        IRoute? _route;
        ObservableCollection<Location?>? _locations;

        public IRoute? Route
        {
            get => _route;
            set
            {
                _route = value;
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
        public RouteEditorDialogViewModel()
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
