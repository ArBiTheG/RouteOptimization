using ReactiveUI;
using RouteOptimization.Models;
using RouteOptimization.Repository;
using RouteOptimization.Repository.SQLite;
using RouteOptimization.ViewModels.Pages.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
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

        public Interaction<RoutesDialogViewModel, IRoute?> ShowDialog { get; }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<IRoute, Unit> EditCommand { get; }
        public ReactiveCommand<IRoute, Unit> DeleteCommand { get; }
        public RoutesPageViewModel()
        {
            _repository = new SQLiteRoutesRepository();

            ShowDialog = new Interaction<RoutesDialogViewModel, IRoute?>();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            AddCommand = ReactiveCommand.CreateFromTask(ExecuteAddCommand);
            EditCommand = ReactiveCommand.CreateFromTask<IRoute>(ExecuteEditCommand);
            DeleteCommand = ReactiveCommand.CreateFromTask<IRoute>(ExecuteDeleteCommand);
        }
        private async Task ExecuteLoadCommand()
        {
            List = new ObservableCollection<IRoute?>(await _repository.GetAll());
        }

        private async Task ExecuteAddCommand()
        {
            var dialog = new RoutesDialogViewModel();

            var result = await ShowDialog.Handle(dialog);
        }
        private async Task ExecuteEditCommand(IRoute location)
        {

        }
        private async Task ExecuteDeleteCommand(IRoute location)
        {

        }
    }
}
