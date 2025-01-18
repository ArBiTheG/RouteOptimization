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
    public class VehicleTypesEditorViewModel : ViewModelBase
    {
        VehicleType _selectedVehicleType;

        public VehicleType SelectedVehicleType
        {
            get => _selectedVehicleType;
            set => this.RaiseAndSetIfChanged(ref _selectedVehicleType, value);
        }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<bool, VehicleType?> ApplyCommand { get; }

        public VehicleTypesEditorViewModel() : this(new()) { }
        public VehicleTypesEditorViewModel(VehicleType vehicleType)
        {
            _selectedVehicleType = vehicleType;

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create<bool, VehicleType?>(ExecuteApplyCommand);
        }

        private async Task ExecuteLoadCommand()
        {
        }
        private VehicleType? ExecuteApplyCommand(bool arg)
        {
            if (arg)
                return SelectedVehicleType;
            return null;
        }
    }
}
