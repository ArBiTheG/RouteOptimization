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
    public class VehicleTypesPageViewModel : ViewModelBase
    {
        IVehicleTypesRepository _repository;
        ObservableCollection<IVehicleType?>? _list;

        public ObservableCollection<IVehicleType?>? List
        {
            get => _list;
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }

        public Interaction<VehicleTypesDialogViewModel, IVehicleType?> ShowDialog { get; }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<IVehicleType, Unit> EditCommand { get; }
        public ReactiveCommand<IVehicleType, Unit> DeleteCommand { get; }

        public VehicleTypesPageViewModel()
        {
            _repository = new SQLiteVehicleTypesRepository();

            ShowDialog = new Interaction<VehicleTypesDialogViewModel, IVehicleType?>();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            AddCommand = ReactiveCommand.CreateFromTask(ExecuteAddCommand);
            EditCommand = ReactiveCommand.CreateFromTask<IVehicleType>(ExecuteEditCommand);
            DeleteCommand = ReactiveCommand.CreateFromTask<IVehicleType>(ExecuteDeleteCommand);
        }

        private async Task ExecuteLoadCommand()
        {
            List = new ObservableCollection<IVehicleType?>(await _repository.GetAll());
        }
        private async Task ExecuteAddCommand()
        {
            var dialog = new VehicleTypesDialogViewModel();

            var result = await ShowDialog.Handle(dialog);
        }
        private async Task ExecuteEditCommand(IVehicleType location)
        {

        }
        private async Task ExecuteDeleteCommand(IVehicleType location)
        {

        }
    }
}
