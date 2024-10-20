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
    public class ShipmentsViewModel : ViewModelBase
    {
        IShipmentsRepository _repository;
        ObservableCollection<Shipment?>? _list;

        public ObservableCollection<Shipment?>? List
        {
            get => _list;
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }

        public Interaction<ShipmentsEditorViewModel, Shipment?> ShowDialog { get; }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<Shipment, Unit> EditCommand { get; }
        public ReactiveCommand<Shipment, Unit> DeleteCommand { get; }

        public ShipmentsViewModel()
        {
            _repository = new SQLiteShipmentsRepository();

            ShowDialog = new Interaction<ShipmentsEditorViewModel, Shipment?>();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            AddCommand = ReactiveCommand.CreateFromTask(ExecuteAddCommand);
            EditCommand = ReactiveCommand.CreateFromTask<Shipment>(ExecuteEditCommand);
            DeleteCommand = ReactiveCommand.CreateFromTask<Shipment>(ExecuteDeleteCommand);
        }

        private async Task ExecuteLoadCommand()
        {
            List = new ObservableCollection<Shipment?>(await _repository.GetAll());
        }

        private async Task ExecuteAddCommand()
        {
            var dialog = new ShipmentsEditorViewModel();

            var result = await ShowDialog.Handle(dialog);
            if (result != null)
            {
                await _repository.Create(result);
                List?.Add(result);
            }
        }
        private async Task ExecuteEditCommand(Shipment shipment)
        {
            var dialog = new ShipmentsEditorViewModel(shipment);

            var result = await ShowDialog.Handle(dialog);
            if (result != null)
            {
                await _repository.Create(result);
                List?.Add(result);
            }
        }
        private async Task ExecuteDeleteCommand(Shipment location)
        {

        }
    }
}
