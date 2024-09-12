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
    public class VehiclesPageViewModel : ViewModelBase
    {
        IVehiclesRepository _repository;
        ObservableCollection<IRoute?>? _list;

        public ObservableCollection<IRoute?>? List
        {
            get => _list;
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }
        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<IVehicle, Unit> EditCommand { get; }
        public ReactiveCommand<IVehicle, Unit> DeleteCommand { get; }

        public VehiclesPageViewModel()
        {
            _repository = new SQLiteVehiclesRepository();

            LoadCommand = ReactiveCommand.Create(ExecuteLoadCommand);
            AddCommand = ReactiveCommand.Create(ExecuteAddCommand);
            EditCommand = ReactiveCommand.Create<IVehicle>(ExecuteEditCommand);
            DeleteCommand = ReactiveCommand.Create<IVehicle>(ExecuteDeleteCommand);
        }

        private void ExecuteLoadCommand()
        {

        }

        private void ExecuteAddCommand()
        {

        }
        private void ExecuteEditCommand(IVehicle location)
        {

        }
        private void ExecuteDeleteCommand(IVehicle location)
        {

        }
    }
}
