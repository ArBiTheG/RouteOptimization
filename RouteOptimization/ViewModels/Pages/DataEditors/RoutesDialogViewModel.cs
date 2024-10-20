using ReactiveUI;
using RouteOptimization.Models;
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
        ILocationsRepository _locationRepository;
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
        public ReactiveCommand<Unit, Route> ApplyCommand { get; }

        public RoutesEditorViewModel()
        {
            _selectedRoute = new();

            _locationRepository = new SQLiteLocationsRepository();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create(ExecuteApplyCommand);
        }

        public RoutesEditorViewModel(Route route)
        {
            _selectedRoute = route;

            _locationRepository = new SQLiteLocationsRepository();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create(ExecuteApplyCommand);
        }

        private async Task ExecuteLoadCommand()
        {
            Locations = new(await _locationRepository.GetAll());
        }
        private Route ExecuteApplyCommand()
        {
            return SelectedRoute;
        }
    }
}
