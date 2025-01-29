using DynamicData;
using Mapsui;
using ReactiveUI;
using RouteOptimization.Models;
using RouteOptimization.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages
{
    public class LoadingViewModel : ViewModelBase
    {
        private LoadingModel _loadingModel;

        private Vehicle? _selectedVehicle;
        private Location? _origin;
        private Location? _destination;
        private DateTime _dateTimeFinish;
        private string _textInfo;

        private ObservableCollection<Cargo?>? _cargosStorage;
        private ObservableCollection<Vehicle?>? _vehicles;
        private ObservableCollection<Location?>? _locations;
        private ObservableCollection<VehicleStatus?>? _vehicleStatuses;
        private ObservableCollection<Cargo?> _cargosCart;

        private bool _isContinueFilling;


        public Vehicle? SelectedVehicle
        {
            get => _selectedVehicle;
            set => this.RaiseAndSetIfChanged(ref _selectedVehicle, value);
        }

        public Location? SelectedOrigin
        {
            get => _origin;
            set => this.RaiseAndSetIfChanged(ref _origin, value);
        }

        public Location? SelectedDestination
        {
            get => _destination;
            set => this.RaiseAndSetIfChanged(ref _destination, value);
        }

        public DateTime SelectedDateTimeFinish
        {
            get => _dateTimeFinish;
            set => this.RaiseAndSetIfChanged(ref _dateTimeFinish, value);
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

        public ObservableCollection<VehicleStatus?>? VehicleStatuses
        {
            get => _vehicleStatuses;
            set => this.RaiseAndSetIfChanged(ref _vehicleStatuses, value);
        }

        public ObservableCollection<Cargo?>? CargosStorage
        {
            get => _cargosStorage;
            set => this.RaiseAndSetIfChanged(ref _cargosStorage, value);
        }
        public ObservableCollection<Cargo?> CargosCart
        {
            get => _cargosCart;
            set => this.RaiseAndSetIfChanged(ref _cargosCart, value);
        }

        public bool IsContinueFilling
        {
            get => _isContinueFilling;
            set => this.RaiseAndSetIfChanged(ref _isContinueFilling, value);
        }

        public string TextInfo
        {
            get => _textInfo;
            set => this.RaiseAndSetIfChanged(ref _textInfo, value);
        }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Unit> ApplyCommand { get; }
        public ReactiveCommand<Unit, Unit> CheckCommand { get; }
        public ReactiveCommand<Location?, Unit> LoadCargosStorageCommand { get; }
        public ReactiveCommand<Cargo?, Unit> CargoToCartCommand { get; }
        public ReactiveCommand<Cargo?, Unit> CargoToStorageCommand { get; }
        public ReactiveCommand<Unit, Unit> CargoToStorageAllCommand { get; }

        public LoadingViewModel()
        {
            _textInfo = "";
            _cargosCart = new();
            _isContinueFilling = false;
            _dateTimeFinish = DateTime.Now;

        LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            LoadCargosStorageCommand = ReactiveCommand.CreateFromTask<Location?>(ExecuteLoadStorageCommand);
            ApplyCommand = ReactiveCommand.CreateFromTask(ExecuteApplyCommand);
            CheckCommand = ReactiveCommand.CreateFromTask(ExecuteCheckCommand);
            CargoToCartCommand = ReactiveCommand.Create<Cargo?>(ExecuteCargoToCartCommand);
            CargoToStorageCommand = ReactiveCommand.Create<Cargo?>(ExecuteCargoToStorageCommand);
            CargoToStorageAllCommand = ReactiveCommand.Create(ExecuteCargoToStorageAllCommand);
        }

        private async Task ExecuteCheckCommand()
        {
            var way = await _loadingModel.Navigate(SelectedOrigin, SelectedDestination);

            if (way != null)
            {
                var text = $"Маршрут от {SelectedOrigin?.Name ?? "Без имени"} до {SelectedDestination?.Name ?? "Без имени"} \n";
                float generalDistance = 0;
                float generalTime = 0;
                int phase = 1;
                foreach (var route in way.Routes)
                {
                    text += $"\n{phase++}.\tот {route.StartLocation?.Name ?? "Без имени"} до {route.FinishLocation?.Name ?? "Без имени"} - {route.Distance} м.";
                    generalDistance += route.Distance;
                    generalTime += route.Time;
                }
                text += $"\n\nПримерное расстояние {generalDistance} м.";
                text += $"\nПримерное время {generalTime} с.";
                TextInfo = text;
            }
        }

        private async Task ExecuteLoadStorageCommand(Location? location)
        {
            IsContinueFilling = false;
            CargosCart.Clear();
            if (_loadingModel != null)
            {
                if (location != null)
                    CargosStorage = new(await _loadingModel.GetCargosByLocationAvailable(location, CargoAvailableValue.Present.Id));
            }

            if (_cargosStorage != null)
                if (_cargosStorage.Count > 0)
                    IsContinueFilling = true;
        }

        private void ExecuteCargoToCartCommand(Cargo? cargo)
        {
            if (CargosStorage !=null)
            {
                if (cargo != null)
                {
                    CargosStorage.Remove(cargo);
                    CargosCart.Add(cargo);
                }
            }
        }

        private void ExecuteCargoToStorageCommand(Cargo? cargo)
        {
            if (CargosStorage != null)
            {
                if (cargo != null)
                {
                    CargosStorage.Add(cargo);
                    CargosCart.Remove(cargo);
                }
            }
        }

        private void ExecuteCargoToStorageAllCommand()
        {
            if (CargosStorage != null)
            {
                CargosStorage.AddRange(CargosCart);
                CargosCart.Clear();
            }
        }

        public LoadingViewModel(LoadingModel model) : this()
        {
            _loadingModel = model;
        }

        private async Task ExecuteLoadCommand()
        {
            if (_loadingModel != null)
            {
                Vehicles = new(await _loadingModel.GetVehicles());
                Locations = new(await _loadingModel.GetLocations());
            }
        }
        private async Task ExecuteApplyCommand()
        {
            if (SelectedVehicle == null) return;
            if (SelectedOrigin == null) return;
            if (SelectedDestination == null) return;
            if (CargosCart.Count <= 0) return;

            if (_loadingModel != null) {
                Shipment prototype = new Shipment();
                prototype.VehicleId = SelectedVehicle.Id;
                prototype.OriginId = SelectedOrigin.Id;
                prototype.DestinationId = SelectedDestination.Id;
                prototype.DateTimeStart = DateTime.Now;
                prototype.DateTimeFinish = SelectedDateTimeFinish;
                prototype.StatusId = ShipmentStatusValue.Moving.Id;

                Vehicle vehicle = SelectedVehicle;
                vehicle.StatusId = VehicleStatusValue.Moving.Id;

                List<Cargo> cargos = new List<Cargo>();
                foreach (Cargo? entity in CargosCart)
                {
                    if (entity != null)
                    {
                        entity.AvailableId = CargoAvailableValue.Moving.Id;

                        cargos.Add(entity);
                    }
                }

                List<Shipment> shipments = new List<Shipment>();
                foreach (Cargo? cargo in CargosCart)
                {
                    if (cargo != null)
                    {
                        Shipment entity = prototype.Clone();
                        entity.Cargo = cargo;
                        entity.CargoId = cargo.Id;

                        shipments.Add(entity);
                    }
                }
                await _loadingModel.CreateShipmentsEditCargosVehicle(shipments, cargos, vehicle);
            }
        }
    }
}
