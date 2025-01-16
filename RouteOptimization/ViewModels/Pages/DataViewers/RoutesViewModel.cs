using ReactiveUI;
using RouteOptimization.Models;
using RouteOptimization.Models.Entities;
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
        RoutesModel _model;
        ObservableCollection<Route?>? _list;

        public ObservableCollection<Route?>? List
        {
            get => _list;
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }

        public Interaction<RoutesEditorViewModel, Route?> ShowDialog { get; }
        public Interaction<DeleteViewModel, bool> ShowDeleteDialog { get; }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<Route, Unit> EditCommand { get; }
        public ReactiveCommand<Route, Unit> DeleteCommand { get; }
        public RoutesViewModel(RoutesModel model)
        {
            _model = model;

            ShowDialog = new Interaction<RoutesEditorViewModel, Route?>();
            ShowDeleteDialog = new Interaction<DeleteViewModel, bool>();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            AddCommand = ReactiveCommand.CreateFromTask(ExecuteAddCommand);
            EditCommand = ReactiveCommand.CreateFromTask<Route>(ExecuteEditCommand);
            DeleteCommand = ReactiveCommand.CreateFromTask<Route>(ExecuteDeleteCommand);
        }
        private async Task ExecuteLoadCommand()
        {
            List = new(await _model.GetAll());
        }

        private async Task ExecuteAddCommand()
        {
            var dialog = new RoutesEditorViewModel(_model);

            var result = await ShowDialog.Handle(dialog);
            if (result != null)
            {
                await _model.Create(result);
                List?.Add(result);
            }
        }
        private async Task ExecuteEditCommand(Route route)
        {
            var dialog = new RoutesEditorViewModel(_model, route);

            var result = await ShowDialog.Handle(dialog);
            if (result != null)
            {
                await _model.Edit(result);
            }
        }
        private async Task ExecuteDeleteCommand(Route route)
        {
            var dialog = new DeleteViewModel();

            var result = await ShowDeleteDialog.Handle(dialog);
            if (result != false)
            {
                await _model.Delete(route);
                List?.Remove(route);
            }
        }
    }
}
