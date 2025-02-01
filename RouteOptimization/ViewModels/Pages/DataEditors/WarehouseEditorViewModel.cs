using ReactiveUI;
using RouteOptimization.Models.Entities;
using RouteOptimization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;

namespace RouteOptimization.ViewModels.Pages.DataEditors
{
    public class WarehouseEditorViewModel: ViewModelBase
    {
        private Cargo _selectedCargo;
        private WarehouseModel _warehouseModel;

        public Cargo SelectedCargo
        {
            get => _selectedCargo;
            set => this.RaiseAndSetIfChanged(ref _selectedCargo, value);
        }
        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<bool, Cargo?> ApplyCommand { get; }
        public WarehouseEditorViewModel()
        {
            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create<bool, Cargo?>(ExecuteApplyCommand);
        }
        public WarehouseEditorViewModel(WarehouseModel model) : this(model, new()) { }
        public WarehouseEditorViewModel(WarehouseModel model, Cargo cargo) : this()
        {
            _warehouseModel = model;
            _selectedCargo = cargo;
        }

        private async Task ExecuteLoadCommand()
        {
        }

        private Cargo? ExecuteApplyCommand(bool arg)
        {
            if (arg)
                return SelectedCargo;
            return null;
        }
    }
}
