using ReactiveUI;
using RouteOptimization.Models;
using RouteOptimization.Repository;
using RouteOptimization.Repository.SQLite;
using RouteOptimization.ViewModels.Pages.DataEditors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages.DataViewers
{
    public class RoutesViewModel : ViewModelBase
    {
        IRoutesRepository _repository;
        ObservableCollection<Route?>? _list;

        public ObservableCollection<Route?>? List
        {
            get => _list;
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }

        public Interaction<RoutesEditorViewModel, Route?> ShowDialog { get; }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<Route, Unit> EditCommand { get; }
        public ReactiveCommand<Route, Unit> DeleteCommand { get; }
        public RoutesViewModel()
        {
            _repository = new SQLiteRoutesRepository();

            ShowDialog = new Interaction<RoutesEditorViewModel, Route?>();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            AddCommand = ReactiveCommand.CreateFromTask(ExecuteAddCommand);
            EditCommand = ReactiveCommand.CreateFromTask<Route>(ExecuteEditCommand);
            DeleteCommand = ReactiveCommand.CreateFromTask<Route>(ExecuteDeleteCommand);
        }
        private async Task ExecuteLoadCommand()
        {
            List = new(await _repository.GetAll());
        }

        private async Task ExecuteAddCommand()
        {
            var dialog = new RoutesEditorViewModel();

            var result = await ShowDialog.Handle(dialog);
            if (result != null)
            {
                await _repository.Create(result);
                List?.Add(result);
            }
        }
        private async Task ExecuteEditCommand(Route route)
        {
            var dialog = new RoutesEditorViewModel(route);

            var result = await ShowDialog.Handle(dialog);
            if (result != null)
            {
                await _repository.Create(result);
                List?.Add(result);
            }
        }
        private async Task ExecuteDeleteCommand(Route location)
        {

        }
    }
}
