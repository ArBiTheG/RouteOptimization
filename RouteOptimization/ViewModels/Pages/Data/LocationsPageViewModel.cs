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
    public class LocationsPageViewModel : ViewModelBase
    {
        ILocationsRepository _repository;
        ObservableCollection<ILocation?>? _list;

        public ObservableCollection<ILocation?>? List 
        { 
            get => _list; 
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<ILocation, Unit> EditCommand { get; }
        public ReactiveCommand<ILocation, Unit> DeleteCommand { get; }
        public LocationsPageViewModel()
        {
            _repository = new SQLiteLocationsRepository();

            LoadCommand = ReactiveCommand.Create(ExecuteLoadCommand);
            AddCommand = ReactiveCommand.Create(ExecuteAddCommand);
            EditCommand = ReactiveCommand.Create<ILocation>(ExecuteEditCommand);
            DeleteCommand = ReactiveCommand.Create<ILocation>(ExecuteDeleteCommand);
        }

        private void ExecuteLoadCommand()
        {
            Task.Run(async () =>
            {
                List = new ObservableCollection<ILocation?>(await _repository.GetAll());
            });
        }

        private void ExecuteAddCommand()
        {
        }
        private void ExecuteEditCommand(ILocation location)
        {

        }
        private void ExecuteDeleteCommand(ILocation location)
        {

        }
    }
}
