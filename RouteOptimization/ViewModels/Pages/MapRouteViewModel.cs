using Mapsui;
using ReactiveUI;
using RouteOptimization.Models;
using RouteOptimization.Models.Entities;
using RouteOptimization.ViewModels.Pages.DataEditors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages
{
    public class MapRouteViewModel: ViewModelBase
    {
        private MapRouteModel _model;
        private Map? _map;

        ObservableCollection<Location?>? _locations;
        Location? _selectedStartLocation;
        Location? _selectedFinishLocation;

        public ObservableCollection<Location?>? Locations
        {
            get => _locations;
            set => this.RaiseAndSetIfChanged(ref _locations, value);
        }

        public Location? SelectedStartLocation
        {
            get => _selectedStartLocation;
            set => this.RaiseAndSetIfChanged(ref _selectedStartLocation, value);
        }

        public Location? SelectedFinishLocation
        {
            get => _selectedFinishLocation;
            set => this.RaiseAndSetIfChanged(ref _selectedFinishLocation, value);
        }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Unit> BuildRouteCommand { get; }
        public Interaction<DeleteViewModel, bool> ShowDeleteDialog { get; }

        public Map? Map
        {
            get => _map;
            set => this.RaiseAndSetIfChanged(ref _map, value);
        }

        public MapRouteViewModel(MapRouteModel model)
        {
            _model = model;
            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            BuildRouteCommand = ReactiveCommand.CreateFromTask(ExecuteBuildRouteCommand);
            ShowDeleteDialog = new Interaction<DeleteViewModel, bool>();
        }

        private async Task ExecuteBuildRouteCommand()
        {
            Map = await _model.Navigate(SelectedStartLocation, SelectedFinishLocation);
        }

        private async Task ExecuteLoadCommand()
        {
            Map = await _model.GetMap();
            Locations = new(await _model.GetLocations());

        }
    }
}
