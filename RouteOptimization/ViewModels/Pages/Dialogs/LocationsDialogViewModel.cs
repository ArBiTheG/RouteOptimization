using ReactiveUI;
using RouteOptimization.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages.Dialogs
{
    public class LocationsDialogViewModel : ViewModelBase
    {
        Location _selectedLocation;

        public Location SelectedLocation
        {
            get => _selectedLocation;
            set => this.RaiseAndSetIfChanged(ref _selectedLocation, value);
        }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Location> ApplyCommand { get; }

        public LocationsDialogViewModel()
        {
            _selectedLocation = new();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create(ExecuteApplyCommand);
        }

        private async Task ExecuteLoadCommand()
        {
        }
        private Location ExecuteApplyCommand()
        {
            return SelectedLocation;
        }
    }
}
