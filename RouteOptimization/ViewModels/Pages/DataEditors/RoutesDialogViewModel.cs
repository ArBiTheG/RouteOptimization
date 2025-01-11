using ReactiveUI;
using RouteOptimization.Models;
using RouteOptimization.Models.Entities;
using RouteOptimization.Repository;
using RouteOptimization.Repository.SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages.DataEditors
{
    public class RoutesEditorViewModel : ViewModelBase
    {
        Route _selectedRoute;
        LocationsModel _locationModel;
        ObservableCollection<Location?>? _locations;

        public Route SelectedRoute
        {
            get => _selectedRoute;
            set => this.RaiseAndSetIfChanged(ref _selectedRoute, value);
        }
        public ObservableCollection<Location?>? Locations
        {
            get => _locations;
            set => this.RaiseAndSetIfChanged(ref _locations, value);
        }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<bool, Route?> ApplyCommand { get; }

        public RoutesEditorViewModel()
        {
            _selectedRoute = new();

            _locationModel = new LocationsModel();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create<bool, Route?>(ExecuteApplyCommand);
        }

        public RoutesEditorViewModel(Route route)
        {
            _selectedRoute = route;

            _locationModel = new LocationsModel();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create<bool, Route?>(ExecuteApplyCommand);
        }

        private async Task ExecuteLoadCommand()
        {
            Locations = new(await _locationModel.GetAll());
        }
        private Route? ExecuteApplyCommand(bool arg)
        {
            if (arg)
                return SelectedRoute;
            return null;
        }
    }
}
