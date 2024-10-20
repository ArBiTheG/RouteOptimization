using ReactiveUI;
using RouteOptimization.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages.DataEditors
{
    public class LocationsEditorViewModel : ViewModelBase
    {
        Location _selectedLocation;

        public Location SelectedLocation
        {
            get => _selectedLocation;
            set => this.RaiseAndSetIfChanged(ref _selectedLocation, value);
        }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<bool, Location?> ApplyCommand { get; }

        public LocationsEditorViewModel()
        {
            _selectedLocation = new();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create<bool, Location?>(ExecuteApplyCommand);
        }

        public LocationsEditorViewModel(Location location)
        {
            _selectedLocation = location;

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create<bool, Location?>(ExecuteApplyCommand);
        }

        private async Task ExecuteLoadCommand()
        {
        }

        private Location? ExecuteApplyCommand(bool arg)
        {
            if (arg)
                return SelectedLocation;
            return null;
        }
    }
}
