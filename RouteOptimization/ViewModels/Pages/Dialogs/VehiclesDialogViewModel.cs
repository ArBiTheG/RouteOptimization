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

namespace RouteOptimization.ViewModels.Pages.Dialogs
{
    public class VehiclesDialogViewModel : ViewModelBase
    {
        Vehicle _selectedVehicle;
        IVehicleStatusesRepository _statusesRepository;
        IVehicleTypesRepository _typesRepository;
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
        public ReactiveCommand<Unit, Vehicle> ApplyCommand { get; }

        public VehiclesDialogViewModel()
        {
            _selectedVehicle = new();

            _statusesRepository = new SQLiteVehicleStatusesRepository();
            _typesRepository = new SQLiteVehicleTypesRepository();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create(ExecuteApplyCommand);
        }
        public VehiclesDialogViewModel(Vehicle vehicle)
        {
            _selectedVehicle = vehicle;

            _statusesRepository = new SQLiteVehicleStatusesRepository();
            _typesRepository = new SQLiteVehicleTypesRepository();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create(ExecuteApplyCommand);
        }

        private async Task ExecuteLoadCommand()
        {
            Types = new(await _typesRepository.GetAll());
            Statuses = new(await _statusesRepository.GetAll());
        }
        private Vehicle ExecuteApplyCommand()
        {
            return SelectedVehicle;
        }
    }
}
