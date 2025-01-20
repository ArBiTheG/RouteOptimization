using Mapsui;
using ReactiveUI;
using RouteOptimization.Models;
using RouteOptimization.Models.Entities;
using RouteOptimization.ViewModels.Pages.DataEditors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RouteOptimization.ViewModels.Pages
{
    public class MapRouteViewModel: ViewModelBase
    {
        private MapRouteModel _model;
        private Map? _map;
        private string _textInfo;

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
        public string TextInfo
        {
            get => _textInfo;
            set => this.RaiseAndSetIfChanged(ref _textInfo, value);
        }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Unit> BuildRouteCommand { get; }
        public ReactiveCommand<Unit, Unit> ClearRouteCommand { get; }
        public Interaction<DeleteViewModel, bool> ShowDeleteDialog { get; }

        public Map? Map
        {
            get => _map;
            set => this.RaiseAndSetIfChanged(ref _map, value);
        }

        public MapRouteViewModel(MapRouteModel model)
        {
            _model = model;
            _textInfo = "";

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            BuildRouteCommand = ReactiveCommand.CreateFromTask(ExecuteBuildRouteCommand);
            ClearRouteCommand = ReactiveCommand.Create(ExecuteClearRouteCommand);
            ShowDeleteDialog = new Interaction<DeleteViewModel, bool>();
        }

        private void ExecuteClearRouteCommand()
        {
            _model.ClearMap();

            Map = _model.GetMap();
            TextInfo = "";
        }

        private async Task ExecuteBuildRouteCommand()
        {
            var way = await _model.Navigate(SelectedStartLocation, SelectedFinishLocation);

            Map = _model.GetMap();
            if (way != null)
            {
                var text = $"Маршрут от {SelectedStartLocation?.Name ?? "Без имени"} до {SelectedFinishLocation?.Name ?? "Без имени"} \n";
                float generalDistance = 0;
                float generalTime = 0;
                int phase = 1;
                foreach (var route in way.Routes)
                {
                    text += $"\n{phase++}.\tот {route.StartLocation?.Name ?? "Без имени"} до {route.FinishLocation?.Name ?? "Без имени"} - {route.Distance} м.";
                    generalDistance += route.Distance;
                    generalTime += route.Time;
                }
                text += $"\n\nПримерное расстояние {generalDistance}";
                text += $"\nПримерное время {generalTime}";
                TextInfo = text;
            }

        }

        private async Task ExecuteLoadCommand()
        {
            Map = _model.GetMap();
            Locations = new(await _model.GetLocations());

        }
    }
}
