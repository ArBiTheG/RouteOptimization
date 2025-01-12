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
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<VehicleType, Unit> EditCommand { get; }
        public ReactiveCommand<VehicleType, Unit> DeleteCommand { get; }

        public VehicleTypesViewModel()
        {
            _model = new VehicleTypesModel();

            ShowDialog = new Interaction<VehicleTypesEditorViewModel, VehicleType?>();
            ShowDeleteDialog = new Interaction<DeleteViewModel, bool>();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            AddCommand = ReactiveCommand.CreateFromTask(ExecuteAddCommand);
            EditCommand = ReactiveCommand.CreateFromTask<VehicleType>(ExecuteEditCommand);
            DeleteCommand = ReactiveCommand.CreateFromTask<VehicleType>(ExecuteDeleteCommand);
        }

        private async Task ExecuteLoadCommand()
        {
            List = new ObservableCollection<VehicleType?>(await _model.GetAll());
        }
        private async Task ExecuteAddCommand()
        {
            var dialog = new VehicleTypesEditorViewModel();

            var result = await ShowDialog.Handle(dialog);
            if (result != null)
            {
                await _model.Create(result);
                List?.Add(result);
            }
        }
        private async Task ExecuteEditCommand(VehicleType vehicleType)
        {
            var dialog = new VehicleTypesEditorViewModel(vehicleType);

            var result = await ShowDialog.Handle(dialog);
            if (result != null)
            {
                await _model.Edit(result);
            }
        }
        private async Task ExecuteDeleteCommand(VehicleType vehicleType)
        {

            var dialog = new DeleteViewModel();

            var result = await ShowDeleteDialog.Handle(dialog);
            if (result != false)
            {
                await _model.Delete(vehicleType);
                List?.Remove(vehicleType);
            }
        }
    }
}
