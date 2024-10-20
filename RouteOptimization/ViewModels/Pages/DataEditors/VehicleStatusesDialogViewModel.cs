using ReactiveUI;
using RouteOptimization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages.DataEditors
{
    public class VehicleStatusesEditorViewModel : ViewModelBase
    {
        VehicleStatus _selectedVehicleStatus;

        public VehicleStatus SelectedVehicleStatus
        {
            get => _selectedVehicleStatus;
            set => this.RaiseAndSetIfChanged(ref _selectedVehicleStatus, value);
        }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, VehicleStatus> ApplyCommand { get; }

        public VehicleStatusesEditorViewModel()
        {
            _selectedVehicleStatus = new();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create(ExecuteApplyCommand);
        }
        public VehicleStatusesEditorViewModel(VehicleStatus vehicleStatus)
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
