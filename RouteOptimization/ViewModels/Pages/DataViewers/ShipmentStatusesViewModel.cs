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
    public class ShipmentStatusesViewModel : ViewModelBase
    {
        ShipmentStatusesModel _model;
        ObservableCollection<ShipmentStatus?>? _list;

        public ObservableCollection<ShipmentStatus?>? List
        {
            get => _list;
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }

        public Interaction<ShipmentStatusesEditorViewModel, ShipmentStatus?> ShowDialog { get; }
        public Interaction<DeleteViewModel, bool> ShowDeleteDialog { get; }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<ShipmentStatus, Unit> EditCommand { get; }

        public ShipmentStatusesViewModel()
        {
            ShowDialog = new Interaction<ShipmentStatusesEditorViewModel, ShipmentStatus?>();
            ShowDeleteDialog = new Interaction<DeleteViewModel, bool>();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            EditCommand = ReactiveCommand.CreateFromTask<ShipmentStatus>(ExecuteEditCommand);
        }
        public ShipmentStatusesViewModel(ShipmentStatusesModel model) : this()
        {
            _model = model;
        }

        private async Task ExecuteLoadCommand()
        {
            List = new ObservableCollection<ShipmentStatus?>(await _model.GetAll());
        }
        private async Task ExecuteEditCommand(ShipmentStatus shipmentStatus)
        {
            var dialog = new ShipmentStatusesEditorViewModel(shipmentStatus.Clone());

            var result = await ShowDialog.Handle(dialog);
            if (result != null)
            {
                shipmentStatus.CopyFrom(result);
                await _model.Edit(shipmentStatus);
            }
        }
    }
}
