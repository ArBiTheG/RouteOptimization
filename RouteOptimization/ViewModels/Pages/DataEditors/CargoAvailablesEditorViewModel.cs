using ReactiveUI;
using RouteOptimization.Models;
using RouteOptimization.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages.DataEditors
{
    public class CargoAvailablesEditorViewModel : ViewModelBase
    {
        CargoAvailable _selectedCargoAvailable;

        public CargoAvailable SelectedCargoAvailable
        {
            get => _selectedCargoAvailable;
            set => this.RaiseAndSetIfChanged(ref _selectedCargoAvailable, value);
        }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<bool, CargoAvailable?> ApplyCommand { get; }

        public CargoAvailablesEditorViewModel() : this(new()) { }
        public CargoAvailablesEditorViewModel(CargoAvailable cargoAvailable)
        {
            _selectedCargoAvailable = cargoAvailable;

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create<bool, CargoAvailable?>(ExecuteApplyCommand);
        }

        private async Task ExecuteLoadCommand()
        {
        }
        private CargoAvailable? ExecuteApplyCommand(bool arg)
        {
            if (arg)
                return SelectedCargoAvailable;
            return null;
        }
    }
}
