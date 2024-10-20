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
    public class VehicleTypesDialogViewModel : ViewModelBase
    {
        VehicleType _selectedVehicleType;

        public VehicleType SelectedVehicleType
        {
            get => _selectedVehicleType;
            set => this.RaiseAndSetIfChanged(ref _selectedVehicleType, value);
        }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, VehicleType> ApplyCommand { get; }

        public VehicleTypesDialogViewModel()
        {
            _selectedVehicleType = new();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create(ExecuteApplyCommand);
        }
        public VehicleTypesDialogViewModel(VehicleType vehicleType)
        {
            _selectedVehicleType = vehicleType;

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create(ExecuteApplyCommand);
        }

        private async Task ExecuteLoadCommand()
        {
        }
        private VehicleType ExecuteApplyCommand()
        {
            return SelectedVehicleType;
        }
    }
}
