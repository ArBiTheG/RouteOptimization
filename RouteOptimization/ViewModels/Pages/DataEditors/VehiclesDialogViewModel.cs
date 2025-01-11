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
    public class VehiclesEditorViewModel : ViewModelBase
    {
        Vehicle _selectedVehicle;
        VehicleStatusesModel _statusesModel;
        VehicleTypesModel _typesModel;
        ObservableCollection<VehicleStatus?>? _statuses;
        ObservableCollection<VehicleType?>? _types;

        public Vehicle SelectedVehicle
        {
            get => _selectedVehicle;
            set => this.RaiseAndSetIfChanged(ref _selectedVehicle, value);
        }
        public ObservableCollection<VehicleStatus?>? Statuses
        {
            get => _statuses;
            set => this.RaiseAndSetIfChanged(ref _statuses, value);
        }
        public ObservableCollection<VehicleType?>? Types
        {
            get => _types;
            set => this.RaiseAndSetIfChanged(ref _types, value);
        }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<bool, Vehicle?> ApplyCommand { get; }

        public VehiclesEditorViewModel()
        {
            _selectedVehicle = new();

            _statusesModel = new VehicleStatusesModel();
            _typesModel = new VehicleTypesModel();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create<bool, Vehicle?>(ExecuteApplyCommand);
        }
        public VehiclesEditorViewModel(Vehicle vehicle)
        {
            _selectedVehicle = vehicle;

            _statusesModel = new VehicleStatusesModel();
            _typesModel = new VehicleTypesModel();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create<bool, Vehicle?>(ExecuteApplyCommand);
        }

        private async Task ExecuteLoadCommand()
        {
            Types = new(await _typesModel.GetAll());
            Statuses = new(await _statusesModel.GetAll());
        }
        private Vehicle? ExecuteApplyCommand(bool arg)
        {
            if (arg)
                return SelectedVehicle;
            return null;
        }
    }
}
