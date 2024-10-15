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
        ObservableCollection<ILocation?>? _list;

        public ObservableCollection<ILocation?>? List 
        { 
            get => _list; 
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }

        public Interaction<LocationsDialogViewModel, ILocation?> ShowDialog { get; }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<ILocation, Unit> EditCommand { get; }
        public ReactiveCommand<ILocation, Unit> DeleteCommand { get; }
        public LocationsPageViewModel()
        {
            _repository = new SQLiteLocationsRepository();

            ShowDialog = new Interaction<LocationsDialogViewModel, ILocation?>();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            AddCommand = ReactiveCommand.CreateFromTask(ExecuteAddCommand);
            EditCommand = ReactiveCommand.CreateFromTask<ILocation>(ExecuteEditCommand);
            DeleteCommand = ReactiveCommand.CreateFromTask<ILocation>(ExecuteDeleteCommand);
        }

        private async Task ExecuteLoadCommand()
        {
            List = new ObservableCollection<ILocation?>(await _repository.GetAll());
        }

        private async Task ExecuteAddCommand()
        {
            var dialog = new LocationsDialogViewModel();

            var result = await ShowDialog.Handle(dialog);
        }
        private async Task ExecuteEditCommand(ILocation location)
        {

        }
        private async Task ExecuteDeleteCommand(ILocation location)
        {

        }
    }
}
