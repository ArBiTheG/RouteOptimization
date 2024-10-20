using ReactiveUI;
using RouteOptimization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages.Dialogs
{
    public class VehicleStatusesDialogViewModel : ViewModelBase
    {
        VehicleStatus _selectedVehicleStatus;

        public VehicleStatus SelectedVehicleStatus
        {
            get => _selectedVehicleStatus;
            set => this.RaiseAndSetIfChanged(ref _selectedVehicleStatus, value);
        }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, VehicleStatus> ApplyCommand { get; }

        public VehicleStatusesDialogViewModel()
        {
            _selectedVehicleStatus = new();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create(ExecuteApplyCommand);
        }
        public VehicleStatusesDialogViewModel(VehicleStatus vehicleStatus)
        {
            _selectedVehicleStatus = vehicleStatus;

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create(ExecuteApplyCommand);
        }

        private async Task ExecuteLoadCommand()
        {
        }
        private VehicleStatus ExecuteApplyCommand()
        {
            return SelectedVehicleStatus;
        }
    }
}
