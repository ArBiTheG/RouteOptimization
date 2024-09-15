using RouteOptimization.WpfApp.Core;
using RouteOptimization.WpfApp.Utilties;
using RouteOptimization.WpfApp.ViewModel.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.ViewModel
{
    public class DatabaseVM: ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public RelayCommand ChangePageCommand { get; }
        public DatabaseVM()
        {
        }
        public DatabaseVM(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            ChangePageCommand = new RelayCommand(ExecuteChangePageCommand);
        }
        public void ExecuteChangePageCommand(object? parameter)
        {
            if (_navigationStore == null) return;
            string? pageName = parameter as string;
            if (pageName != null)
            {
                switch (pageName)
                {
                    case "Locations":
                        _navigationStore.CurrentViewModel = new LocationsViewModel();
                        break;
                    case "Routes":
                        _navigationStore.CurrentViewModel = new RoutesViewModel();
                        break;
                    case "Shipments":
                        _navigationStore.CurrentViewModel = new ShipmentsViewModel();
                        break;
                    case "Vehicles":
                        _navigationStore.CurrentViewModel = new VehiclesViewModel();
                        break;
                    case "VehicleStatuses":
                        _navigationStore.CurrentViewModel = new VehicleStatusesViewModel();
                        break;
                    case "VehicleTypes":
                        _navigationStore.CurrentViewModel = new VehicleTypesViewModel();
                        break;
                }
            }
        }
    }
}
