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
    public class VehicleStatusesViewModel : ViewModelBase
    {
        VehicleStatusesModel _model;
        ObservableCollection<VehicleStatus?>? _list;

        public ObservableCollection<VehicleStatus?>? List
        {
            get => _list;
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }

        public Interaction<VehicleStatusesEditorViewModel, VehicleStatus?> ShowDialog { get; }
        public Interaction<DeleteViewModel, bool> ShowDeleteDialog { get; }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<VehicleStatus, Unit> EditCommand { get; }

        public VehicleStatusesViewModel()
        {
            ShowDialog = new Interaction<VehicleStatusesEditorViewModel, VehicleStatus?>();
            ShowDeleteDialog = new Interaction<DeleteViewModel, bool>();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            EditCommand = ReactiveCommand.CreateFromTask<VehicleStatus>(ExecuteEditCommand);
        }
        public VehicleStatusesViewModel(VehicleStatusesModel model) :this()
        {
            _model = model;
        }


        private async Task ExecuteLoadCommand()
        {
            List = new ObservableCollection<VehicleStatus?>(await _model.GetAll());
        }

        private async Task ExecuteEditCommand(VehicleStatus vehicleStatus)
        {
            var dialog = new VehicleStatusesEditorViewModel(vehicleStatus.Clone());

            var result = await ShowDialog.Handle(dialog);
            if (result != null)
            {
                vehicleStatus.CopyFrom(result);
                await _model.Edit(vehicleStatus);
            }
        }
    }
}
