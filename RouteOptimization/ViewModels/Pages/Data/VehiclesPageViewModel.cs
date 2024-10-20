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
        ObservableCollection<Vehicle?>? _list;

        public ObservableCollection<Vehicle?>? List
        {
            get => _list;
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }

        public Interaction<VehiclesDialogViewModel, Vehicle?> ShowDialog { get; }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<Vehicle, Unit> EditCommand { get; }
        public ReactiveCommand<Vehicle, Unit> DeleteCommand { get; }

        public VehiclesPageViewModel()
        {
            _repository = new SQLiteVehiclesRepository();

            ShowDialog = new Interaction<VehiclesDialogViewModel, Vehicle?>();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            AddCommand = ReactiveCommand.CreateFromTask(ExecuteAddCommand);
            EditCommand = ReactiveCommand.CreateFromTask<Vehicle>(ExecuteEditCommand);
            DeleteCommand = ReactiveCommand.CreateFromTask<Vehicle>(ExecuteDeleteCommand);
        }

        private async Task ExecuteLoadCommand()
        {
            List = new ObservableCollection<Vehicle?>(await _repository.GetAll());
        }

        private async Task ExecuteAddCommand()
        {
            var dialog = new VehiclesDialogViewModel();

            var result = await ShowDialog.Handle(dialog);
            if (result != null)
            {
                await _repository.Create(result);
                List?.Add(result);
            }
        }
        private async Task ExecuteEditCommand(Vehicle location)
        {

        }
        private async Task ExecuteDeleteCommand(Vehicle location)
        {

        }
    }
}
