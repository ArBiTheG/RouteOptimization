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
    public class CargoAvailablesViewModel : ViewModelBase
    {
        CargoAvailablesModel _model;
        ObservableCollection<CargoAvailable?>? _list;

        public ObservableCollection<CargoAvailable?>? List
        {
            get => _list;
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }

        public Interaction<CargoAvailablesEditorViewModel, CargoAvailable?> ShowDialog { get; }
        public Interaction<DeleteViewModel, bool> ShowDeleteDialog { get; }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<CargoAvailable, Unit> EditCommand { get; }

        public CargoAvailablesViewModel()
        {
            ShowDialog = new Interaction<CargoAvailablesEditorViewModel, CargoAvailable?>();
            ShowDeleteDialog = new Interaction<DeleteViewModel, bool>();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            EditCommand = ReactiveCommand.CreateFromTask<CargoAvailable>(ExecuteEditCommand);
        }
        public CargoAvailablesViewModel(CargoAvailablesModel model) : this()
        {
            _model = model;
        }

        private async Task ExecuteLoadCommand()
        {
            List = new ObservableCollection<CargoAvailable?>(await _model.GetAll());
        }
        private async Task ExecuteEditCommand(CargoAvailable cargoAvailable)
        {
            var dialog = new CargoAvailablesEditorViewModel(cargoAvailable.Clone());

            var result = await ShowDialog.Handle(dialog);
            if (result != null)
            {
                cargoAvailable.CopyFrom(result);
                await _model.Edit(cargoAvailable);
            }
        }
    }
}
