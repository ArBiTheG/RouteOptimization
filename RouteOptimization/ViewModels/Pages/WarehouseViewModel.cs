using ReactiveUI;
using RouteOptimization.Models;
using RouteOptimization.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

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

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Location?, Unit> LoadCargosStorageCommand { get; }

        public WarehouseViewModel()
        {
            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            LoadCargosStorageCommand = ReactiveCommand.CreateFromTask<Location?>(ExecuteLoadStorageCommand);
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
