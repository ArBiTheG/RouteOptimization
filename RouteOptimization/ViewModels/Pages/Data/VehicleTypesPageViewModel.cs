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
        ObservableCollection<VehicleType?>? _list;

        public ObservableCollection<VehicleType?>? List
        {
            get => _list;
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }

        public Interaction<VehicleTypesDialogViewModel, VehicleType?> ShowDialog { get; }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<VehicleType, Unit> EditCommand { get; }
        public ReactiveCommand<VehicleType, Unit> DeleteCommand { get; }

        public VehicleTypesPageViewModel()
        {
            _repository = new SQLiteVehicleTypesRepository();

            ShowDialog = new Interaction<VehicleTypesDialogViewModel, VehicleType?>();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            AddCommand = ReactiveCommand.CreateFromTask(ExecuteAddCommand);
            EditCommand = ReactiveCommand.CreateFromTask<VehicleType>(ExecuteEditCommand);
            DeleteCommand = ReactiveCommand.CreateFromTask<VehicleType>(ExecuteDeleteCommand);
        }

        private async Task ExecuteLoadCommand()
        {
            List = new ObservableCollection<VehicleType?>(await _repository.GetAll());
        }
        private async Task ExecuteAddCommand()
        {
            var dialog = new VehicleTypesDialogViewModel();

            var result = await ShowDialog.Handle(dialog);
            if (result != null)
            {
                await _repository.Create(result);
                List?.Add(result);
            }
        }
        private async Task ExecuteEditCommand(VehicleType location)
        {

        }
        private async Task ExecuteDeleteCommand(VehicleType location)
        {

        }
    }
}
