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
    public class VehicleTypesViewModel : ViewModelBase
    {
        VehicleTypesModel _model;
        ObservableCollection<VehicleType?>? _list;

        public ObservableCollection<VehicleType?>? List
        {
            get => _list;
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }

        public Interaction<VehicleTypesEditorViewModel, VehicleType?> ShowDialog { get; }
        public Interaction<DeleteViewModel, bool> ShowDeleteDialog { get; }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<VehicleType, Unit> EditCommand { get; }

        public VehicleTypesViewModel()
        {

            ShowDialog = new Interaction<VehicleTypesEditorViewModel, VehicleType?>();
            ShowDeleteDialog = new Interaction<DeleteViewModel, bool>();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            EditCommand = ReactiveCommand.CreateFromTask<VehicleType>(ExecuteEditCommand);
        }
        public VehicleTypesViewModel(VehicleTypesModel model):this()
        {
            _model = model;
        }

        private async Task ExecuteLoadCommand()
        {
            List = new ObservableCollection<VehicleType?>(await _model.GetAll());
        }
        private async Task ExecuteEditCommand(VehicleType vehicleType)
        {
            var dialog = new VehicleTypesEditorViewModel(vehicleType.Clone());

            var result = await ShowDialog.Handle(dialog);
            if (result != null)
            {
                vehicleType.CopyFrom(result);
                await _model.Edit(vehicleType);
            }
        }
    }
}
