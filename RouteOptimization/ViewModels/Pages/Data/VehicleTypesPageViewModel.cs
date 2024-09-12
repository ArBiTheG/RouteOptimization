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
    public class VehicleTypesPageViewModel : ViewModelBase
    {
        IVehicleTypesRepository _repository;
        ObservableCollection<IRoute?>? _list;

        public ObservableCollection<IRoute?>? List
        {
            get => _list;
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }
        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<IVehicleType, Unit> EditCommand { get; }
        public ReactiveCommand<IVehicleType, Unit> DeleteCommand { get; }

        public VehicleTypesPageViewModel()
        {
            _repository = new SQLiteVehicleTypesRepository();

            LoadCommand = ReactiveCommand.Create(ExecuteLoadCommand);
            AddCommand = ReactiveCommand.Create(ExecuteAddCommand);
            EditCommand = ReactiveCommand.Create<IVehicleType>(ExecuteEditCommand);
            DeleteCommand = ReactiveCommand.Create<IVehicleType>(ExecuteDeleteCommand);
        }

        private void ExecuteLoadCommand()
        {

        }
        private void ExecuteAddCommand()
        {

        }
        private void ExecuteEditCommand(IVehicleType location)
        {

        }
        private void ExecuteDeleteCommand(IVehicleType location)
        {

        }
    }
}
