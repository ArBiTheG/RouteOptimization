using ReactiveUI;
using RouteOptimization.Models;
using RouteOptimization.Repository;
using RouteOptimization.Repository.SQLite;
using RouteOptimization.ViewModels.Pages.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages.Data
{
    public class VehicleStatusesPageViewModel : ViewModelBase
    {
        IVehicleStatusesRepository _repository;
        ObservableCollection<IVehicleStatus?>? _list;

        public ObservableCollection<IVehicleStatus?>? List
        {
            get => _list;
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }

        public Interaction<VehicleStatusesDialogViewModel, IVehicleStatus?> ShowDialog { get; }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<IVehicleStatus, Unit> EditCommand { get; }
        public ReactiveCommand<IVehicleStatus, Unit> DeleteCommand { get; }

        public VehicleStatusesPageViewModel()
        {
            _repository = new SQLiteVehicleStatusesRepository();

            ShowDialog = new Interaction<VehicleStatusesDialogViewModel, IVehicleStatus?>();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            AddCommand = ReactiveCommand.CreateFromTask(ExecuteAddCommand);
            EditCommand = ReactiveCommand.CreateFromTask<IVehicleStatus>(ExecuteEditCommand);
            DeleteCommand = ReactiveCommand.CreateFromTask<IVehicleStatus>(ExecuteDeleteCommand);
        }


        private async Task ExecuteLoadCommand()
        {
            List = new ObservableCollection<IVehicleStatus?>(await _repository.GetAll());
        }

        private async Task ExecuteAddCommand()
        {
            var dialog = new VehicleStatusesDialogViewModel();

            var result = await ShowDialog.Handle(dialog);
        }
        private async Task ExecuteEditCommand(IVehicleStatus location)
        {

        }
        private async Task ExecuteDeleteCommand(IVehicleStatus location)
        {

        }
    }
}
