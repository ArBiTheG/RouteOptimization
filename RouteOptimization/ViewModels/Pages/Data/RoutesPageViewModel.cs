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
    public class RoutesPageViewModel : ViewModelBase
    {
        IRoutesRepository _repository;
        ObservableCollection<IRoute?>? _list;

        public ObservableCollection<IRoute?>? List
        {
            get => _list;
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }
        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<IRoute, Unit> EditCommand { get; }
        public ReactiveCommand<IRoute, Unit> DeleteCommand { get; }
        public RoutesPageViewModel()
        {
            _repository = new SQLiteRoutesRepository();

            LoadCommand = ReactiveCommand.Create(ExecuteLoadCommand);
            AddCommand = ReactiveCommand.Create(ExecuteAddCommand);
            EditCommand = ReactiveCommand.Create<IRoute>(ExecuteEditCommand);
            DeleteCommand = ReactiveCommand.Create<IRoute>(ExecuteDeleteCommand);
        }
        private void ExecuteLoadCommand()
        {
            Task.Run(async () =>
            {
                List = new ObservableCollection<IRoute?>(await _repository.GetAll());
            });
        }

        private void ExecuteAddCommand()
        {

        }
        private void ExecuteEditCommand(IRoute location)
        {

        }
        private void ExecuteDeleteCommand(IRoute location)
        {

        }
    }
}
