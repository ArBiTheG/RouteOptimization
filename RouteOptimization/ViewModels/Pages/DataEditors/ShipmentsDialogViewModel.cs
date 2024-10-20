using ReactiveUI;
using RouteOptimization.Models;
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
        Shipment _selectedShipment;
        ILocationsRepository _locationRepository;
        ObservableCollection<Location?>? _locations;

        public Shipment SelectedShipment
        {
            get => _selectedShipment;
            set => this.RaiseAndSetIfChanged(ref _selectedShipment, value);
        }
        public ObservableCollection<Location?>? Locations
        {
            get => _locations;
            set => this.RaiseAndSetIfChanged(ref _locations, value);
        }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Shipment> ApplyCommand { get; }

        public ShipmentsEditorViewModel()
        {
            _selectedShipment = new();

            _locationRepository = new SQLiteLocationsRepository();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create(ExecuteApplyCommand);

        }

        public ShipmentsEditorViewModel(Shipment shipment)
        {
            _selectedShipment = shipment;

            _locationRepository = new SQLiteLocationsRepository();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create(ExecuteApplyCommand);
        }

        private async Task ExecuteLoadCommand()
        {
            Locations = new(await _locationRepository.GetAll());
        }
        private Shipment ExecuteApplyCommand()
        {
            return SelectedShipment;
        }
    }
}
