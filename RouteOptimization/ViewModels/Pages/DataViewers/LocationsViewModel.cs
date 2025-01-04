using ReactiveUI;
using RouteOptimization.Models.Entities;
using RouteOptimization.Repository;
using RouteOptimization.Repository.SQLite;
using RouteOptimization.ViewModels.Pages.DataEditors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages.DataViewers
{
    public class LocationsViewModel : ViewModelBase
    {
        ILocationsRepository _repository;
        ObservableCollection<Location?>? _list;

        public ObservableCollection<Location?>? List 
        { 
            get => _list; 
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }

        public Interaction<LocationsEditorViewModel, Location?> ShowDialog { get; }
        public Interaction<DeleteViewModel, bool> ShowDeleteDialog { get; }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<Location, Unit> EditCommand { get; }
        public ReactiveCommand<Location, Unit> DeleteCommand { get; }
        public LocationsViewModel()
        {
            _repository = new SQLiteLocationsRepository();

            ShowDialog = new Interaction<LocationsEditorViewModel, Location?>();
            ShowDeleteDialog = new Interaction<DeleteViewModel, bool>();

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
            var dialog = new LocationsEditorViewModel();

            var result = await ShowDialog.Handle(dialog);
            if (result != null)
            {
                await _repository.Create(result);
                List?.Add(result);
            }
        }
        private async Task ExecuteEditCommand(Location location)
        {
            var dialog = new LocationsEditorViewModel(location);

            var result = await ShowDialog.Handle(dialog);
            if (result != null)
            {
                await _repository.Edit(result);
            }
        }
        private async Task ExecuteDeleteCommand(Location location)
        {
            var dialog = new DeleteViewModel();

            var result = await ShowDeleteDialog.Handle(dialog);
            if (result != false)
            {
                await _repository.Delete(location);
                List?.Remove(location);
            }
        }
    }
}
