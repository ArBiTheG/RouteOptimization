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

namespace RouteOptimization.ViewModels.Pages.Data
{
    public class VehicleStatusesPageViewModel : ViewModelBase
    {
        IVehicleStatusesRepository _repository;
        ObservableCollection<IRoute?>? _list;

        public ObservableCollection<IRoute?>? List
        {
            get => _list;
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }
        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<IVehicleStatus, Unit> EditCommand { get; }
        public ReactiveCommand<IVehicleStatus, Unit> DeleteCommand { get; }

        public VehicleStatusesPageViewModel()
        {
            _repository = new SQLiteVehicleStatusesRepository();

            LoadCommand = ReactiveCommand.Create(ExecuteLoadCommand);
            AddCommand = ReactiveCommand.Create(ExecuteAddCommand);
            EditCommand = ReactiveCommand.Create<IVehicleStatus>(ExecuteEditCommand);
            DeleteCommand = ReactiveCommand.Create<IVehicleStatus>(ExecuteDeleteCommand);
        }


        private void ExecuteLoadCommand()
        {

        }

        private void ExecuteAddCommand()
        {

        }
        private void ExecuteEditCommand(IVehicleStatus location)
        {

        }
        private void ExecuteDeleteCommand(IVehicleStatus location)
        {

        }
    }
}
