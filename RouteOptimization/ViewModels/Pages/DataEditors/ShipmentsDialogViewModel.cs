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
        Shipment _selectedShipment;
        ShipmentsModel _shipmentsModel;
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
        public ReactiveCommand<bool, Shipment?> ApplyCommand { get; }

        public ShipmentsEditorViewModel() : this(new()) { }
        public ShipmentsEditorViewModel(Shipment shipment)
        {
            _selectedShipment = shipment;

            _shipmentsModel = new ShipmentsModel();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create<bool, Shipment?>(ExecuteApplyCommand);
        }

        private async Task ExecuteLoadCommand()
        {
            Locations = new(await _shipmentsModel.GetLocations());
        }
        private Shipment? ExecuteApplyCommand(bool arg)
        {
            if (arg)
                return SelectedShipment;
            return null;
        }
    }
}
