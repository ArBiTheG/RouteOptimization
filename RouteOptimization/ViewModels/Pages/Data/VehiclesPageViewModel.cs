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
    public class VehiclesPageViewModel : ViewModelBase
    {
        IVehiclesRepository _repository;
        ObservableCollection<IVehicle?>? _list;

        public ObservableCollection<IVehicle?>? List
        {
            get => _list;
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }

        public Interaction<VehiclesDialogViewModel, IVehicle?> ShowDialog { get; }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<IVehicle, Unit> EditCommand { get; }
        public ReactiveCommand<IVehicle, Unit> DeleteCommand { get; }

        public VehiclesPageViewModel()
        {
            _repository = new SQLiteVehiclesRepository();

            ShowDialog = new Interaction<VehiclesDialogViewModel, IVehicle?>();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            AddCommand = ReactiveCommand.CreateFromTask(ExecuteAddCommand);
            EditCommand = ReactiveCommand.CreateFromTask<IVehicle>(ExecuteEditCommand);
            DeleteCommand = ReactiveCommand.CreateFromTask<IVehicle>(ExecuteDeleteCommand);
        }

        private async Task ExecuteLoadCommand()
        {
            List = new ObservableCollection<IVehicle?>(await _repository.GetAll());
        }

        private async Task ExecuteAddCommand()
        {
            var dialog = new VehiclesDialogViewModel();

            var result = await ShowDialog.Handle(dialog);
        }
        private async Task ExecuteEditCommand(IVehicle location)
        {

        }
        private async Task ExecuteDeleteCommand(IVehicle location)
        {

        }
    }
}
