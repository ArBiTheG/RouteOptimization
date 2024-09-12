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
    public class ShipmentsPageViewModel : ViewModelBase
    {
        IShipmentsRepository _repository;
        ObservableCollection<IRoute?>? _list;

        public ObservableCollection<IRoute?>? List
        {
            get => _list;
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<IShipment, Unit> EditCommand { get; }
        public ReactiveCommand<IShipment, Unit> DeleteCommand { get; }

        public ShipmentsPageViewModel()
        {
            _repository = new SQLiteShipmentsRepository();

            LoadCommand = ReactiveCommand.Create(ExecuteLoadCommand);
            AddCommand = ReactiveCommand.Create(ExecuteAddCommand);
            EditCommand = ReactiveCommand.Create<IShipment>(ExecuteEditCommand);
            DeleteCommand = ReactiveCommand.Create<IShipment>(ExecuteDeleteCommand);
        }

        private void ExecuteLoadCommand()
        {

        }

        private void ExecuteAddCommand()
        {

        }
        private void ExecuteEditCommand(IShipment location)
        {

        }
        private void ExecuteDeleteCommand(IShipment location)
        {

        }
    }
}
