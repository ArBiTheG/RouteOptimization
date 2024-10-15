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
    public class ShipmentsPageViewModel : ViewModelBase
    {
        IShipmentsRepository _repository;
        ObservableCollection<IShipment?>? _list;

        public ObservableCollection<IShipment?>? List
        {
            get => _list;
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }

        public Interaction<ShipmentsDialogViewModel, IShipment?> ShowDialog { get; }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<IShipment, Unit> EditCommand { get; }
        public ReactiveCommand<IShipment, Unit> DeleteCommand { get; }

        public ShipmentsPageViewModel()
        {
            _repository = new SQLiteShipmentsRepository();

            ShowDialog = new Interaction<ShipmentsDialogViewModel, IShipment?>();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            AddCommand = ReactiveCommand.CreateFromTask(ExecuteAddCommand);
            EditCommand = ReactiveCommand.CreateFromTask<IShipment>(ExecuteEditCommand);
            DeleteCommand = ReactiveCommand.CreateFromTask<IShipment>(ExecuteDeleteCommand);
        }

        private async Task ExecuteLoadCommand()
        {
            List = new ObservableCollection<IShipment?>(await _repository.GetAll());
        }

        private async Task ExecuteAddCommand()
        {
            var dialog = new ShipmentsDialogViewModel();

            var result = await ShowDialog.Handle(dialog);
        }
        private async Task ExecuteEditCommand(IShipment location)
        {

        }
        private async Task ExecuteDeleteCommand(IShipment location)
        {

        }
    }
}
