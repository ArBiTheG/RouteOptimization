using ReactiveUI;
using RouteOptimization.Models;
using RouteOptimization.Models.Entities;
using RouteOptimization.Repository;
using RouteOptimization.Repository.SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages.DataEditors
{
    public class ShipmentsEditorViewModel : ViewModelBase
    {
        private Shipment _selectedShipment;
        private ShipmentsModel _shipmentsModel;
        private ObservableCollection<Cargo?>? _cargos;
        private ObservableCollection<Vehicle?>? _vehicles;
        private ObservableCollection<Location?>? _locations;
        private ObservableCollection<ShipmentStatus?>? _statuses;

        public Shipment SelectedShipment
        {
            get => _selectedShipment;
            set => this.RaiseAndSetIfChanged(ref _selectedShipment, value);
        }
        public ObservableCollection<Cargo?>? Cargos
        {
            get => _cargos;
            set => this.RaiseAndSetIfChanged(ref _cargos, value);
        }
        public ObservableCollection<Vehicle?>? Vehicles
        {
            get => _vehicles;
            set => this.RaiseAndSetIfChanged(ref _vehicles, value);
        }
        public ObservableCollection<Location?>? Locations
        {
            get => _locations;
            set => this.RaiseAndSetIfChanged(ref _locations, value);
        }
        public ObservableCollection<ShipmentStatus?>? Statuses
        {
            get => _statuses;
            set => this.RaiseAndSetIfChanged(ref _statuses, value);
        }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<bool, Shipment?> ApplyCommand { get; }

        public ShipmentsEditorViewModel()
        {
            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create<bool, Shipment?>(ExecuteApplyCommand);
        }

        public ShipmentsEditorViewModel(ShipmentsModel model) : this(model, new()) { }
        public ShipmentsEditorViewModel(ShipmentsModel model, Shipment shipment) : this()
        {
            _shipmentsModel = model;
            _selectedShipment = shipment;
        }

        private async Task ExecuteLoadCommand()
        {
            Cargos = new(await _shipmentsModel.GetCargos());
            Vehicles = new(await _shipmentsModel.GetVehicles());
            Locations = new(await _shipmentsModel.GetLocations());
            Statuses = new(await _shipmentsModel.GetStatuses());
        }
        private Shipment? ExecuteApplyCommand(bool arg)
        {
            if (arg)
                return SelectedShipment;
            return null;
        }
    }
}
