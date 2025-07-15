using ReactiveUI;
using RouteOptimization.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages.DataEditors
{
    public class ShipmentStatusesEditorViewModel : ViewModelBase
    {
        ShipmentStatus _selectedShipmentStatus;

        public ShipmentStatus SelectedShipmentStatus
        {
            get => _selectedShipmentStatus;
            set => this.RaiseAndSetIfChanged(ref _selectedShipmentStatus, value);
        }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<bool, ShipmentStatus?> ApplyCommand { get; }

        public ShipmentStatusesEditorViewModel() : this(new()) { }
        public ShipmentStatusesEditorViewModel(ShipmentStatus shipmentStatus)
        {
            _selectedShipmentStatus = shipmentStatus;

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create<bool, ShipmentStatus?>(ExecuteApplyCommand);
        }

        private async Task ExecuteLoadCommand()
        {
        }
        private ShipmentStatus? ExecuteApplyCommand(bool arg)
        {
            if (arg)
                return SelectedShipmentStatus;
            return null;
        }
    }
}
