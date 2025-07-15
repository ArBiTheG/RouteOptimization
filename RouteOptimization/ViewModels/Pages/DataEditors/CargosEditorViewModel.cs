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
    public class CargosEditorViewModel : ViewModelBase
    {
        private Cargo _selectedCargo;
        private CargosModel _cargosModel;
        private ObservableCollection<CargoAvailable?>? _availables;
        private ObservableCollection<Location?>? _locations;

        public Cargo SelectedCargo
        {
            get => _selectedCargo;
            set => this.RaiseAndSetIfChanged(ref _selectedCargo, value);
        }
        public ObservableCollection<CargoAvailable?>? Availables
        {
            get => _availables;
            set => this.RaiseAndSetIfChanged(ref _availables, value);
        }
        public ObservableCollection<Location?>? Locations
        {
            get => _locations;
            set => this.RaiseAndSetIfChanged(ref _locations, value);
        }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<bool, Cargo?> ApplyCommand { get; }

        public CargosEditorViewModel()
        {
            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create<bool, Cargo?>(ExecuteApplyCommand);
        }
        public CargosEditorViewModel(CargosModel model) : this(model, new()) { }
        public CargosEditorViewModel(CargosModel model, Cargo cargo) : this()
        {
            _cargosModel = model;
            _selectedCargo = cargo;
        }

        private async Task ExecuteLoadCommand()
        {
            Availables = new(await _cargosModel.GetAvailables());
            Locations = new(await _cargosModel.GetLocations());
        }

        private Cargo? ExecuteApplyCommand(bool arg)
        {
            if (arg)
                return SelectedCargo;
            return null;
        }
    }
}
