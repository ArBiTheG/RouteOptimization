using ReactiveUI;
using RouteOptimization.Models;
using RouteOptimization.Models.Entities;
using RouteOptimization.ViewModels.Pages.DataEditors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using static SkiaSharp.HarfBuzz.SKShaper;

namespace RouteOptimization.ViewModels.Pages
{
    public class WarehouseViewModel : ViewModelBase
    {
        private WarehouseModel _warehouseModel;

        private ObservableCollection<Location?>? _locations;

        private ObservableCollection<Cargo?>? _cargos;

        public ObservableCollection<Location?>? Locations
        {
            get => _locations;
            set => this.RaiseAndSetIfChanged(ref _locations, value);
        }
        public ObservableCollection<Cargo?>? Cargos
        {
            get => _cargos;
            set => this.RaiseAndSetIfChanged(ref _cargos, value);
        }

        private Location? _selectedLocation;
        public Location? SelectedLocation
        {
            get => _selectedLocation;
            set => this.RaiseAndSetIfChanged(ref _selectedLocation, value);
        }
        private Cargo _selectedCargo;
        public Cargo SelectedCargo
        {
            get => _selectedCargo;
            set => this.RaiseAndSetIfChanged(ref _selectedCargo, value);
        }

        public Interaction<WarehouseEditorViewModel, Cargo?> ShowDialog { get; }
        public Interaction<DeleteViewModel, bool> ShowDeleteDialog { get; }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Location?, Unit> LoadCargosStorageCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<Cargo, Unit> EditCommand { get; }
        public ReactiveCommand<Cargo, Unit> SellCommand { get; }

        public WarehouseViewModel()
        {
            _selectedCargo = new();
            ShowDialog = new Interaction<WarehouseEditorViewModel, Cargo?>();
            ShowDeleteDialog = new Interaction<DeleteViewModel, bool>();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            LoadCargosStorageCommand = ReactiveCommand.CreateFromTask<Location?>(ExecuteLoadStorageCommand);
            AddCommand = ReactiveCommand.CreateFromTask(ExecuteAddCommand);
            EditCommand = ReactiveCommand.CreateFromTask<Cargo>(ExecuteEditCommand);
            SellCommand = ReactiveCommand.CreateFromTask<Cargo>(ExecuteSellCommand);
        }

        private async Task ExecuteSellCommand(Cargo cargo)
        {
            await _warehouseModel.Sell(cargo);
            Cargos?.Remove(cargo);
        }

        private async Task ExecuteEditCommand(Cargo cargo)
        {
            var dialog = new WarehouseEditorViewModel(_warehouseModel, cargo);

            var result = await ShowDialog.Handle(dialog);
            if (result != null)
            {
                await _warehouseModel.Edit(result);
            }
        }

        private async Task ExecuteAddCommand()
        {
            if (_selectedLocation == null) return;
            var dialog = new WarehouseEditorViewModel(_warehouseModel);

            var result = await ShowDialog.Handle(dialog);
            if (result != null)
            {
                await _warehouseModel.Create(result, _selectedLocation);
                Cargos?.Add(result);
            }
        }

        public WarehouseViewModel(WarehouseModel warehouseModel): this()
        {
            _warehouseModel = warehouseModel;
        }

        private async Task ExecuteLoadCommand()
        {
            Locations = new(await _warehouseModel.GetLocations());
        }

        private async Task ExecuteLoadStorageCommand(Location? location)
        {
            if (location != null)
                Cargos = new(await _warehouseModel.GetCargosByLocationAvailable(location, CargoAvailableValue.Present.Id));
            else
                Cargos = new();
        }
    }
}
