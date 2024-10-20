using ReactiveUI;
using RouteOptimization.Models;
using RouteOptimization.Repository;
using RouteOptimization.Repository.SQLite;
using RouteOptimization.ViewModels.Pages.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages.Data
{
    public class LocationsPageViewModel : ViewModelBase
    {
        ILocationsRepository _repository;
        ObservableCollection<Location?>? _list;

        public ObservableCollection<Location?>? List 
        { 
            get => _list; 
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }

        public Interaction<LocationsDialogViewModel, Location?> ShowDialog { get; }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<Location, Unit> EditCommand { get; }
        public ReactiveCommand<Location, Unit> DeleteCommand { get; }
        public LocationsPageViewModel()
        {
            _repository = new SQLiteLocationsRepository();

            ShowDialog = new Interaction<LocationsDialogViewModel, Location?>();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            AddCommand = ReactiveCommand.CreateFromTask(ExecuteAddCommand);
            EditCommand = ReactiveCommand.CreateFromTask<Location>(ExecuteEditCommand);
            DeleteCommand = ReactiveCommand.CreateFromTask<Location>(ExecuteDeleteCommand);
        }

        private async Task ExecuteLoadCommand()
        {
            List = new(await _repository.GetAll());
        }

        private async Task ExecuteAddCommand()
        {
            var dialog = new LocationsDialogViewModel();

            var result = await ShowDialog.Handle(dialog);
            if (result != null)
            {
                await _repository.Create(result);
                List?.Add(result);
            }
        }
        private async Task ExecuteEditCommand(Location location)
        {
            var dialog = new LocationsDialogViewModel(location);

            var result = await ShowDialog.Handle(dialog);
            if (result != null)
            {
                await _repository.Create(result);
                List?.Add(result);
            }
        }
        private async Task ExecuteDeleteCommand(Location location)
        {

        }
    }
}
