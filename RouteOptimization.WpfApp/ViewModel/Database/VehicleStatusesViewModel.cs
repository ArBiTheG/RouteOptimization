using RouteOptimization.WpfApp.Models;
using RouteOptimization.WpfApp.Repository;
using RouteOptimization.WpfApp.Repository.SQLite;
using RouteOptimization.WpfApp.Utilties;
using RouteOptimization.WpfApp.View.Dialogs;
using RouteOptimization.WpfApp.ViewModel.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.ViewModel.Database
{
    public class VehicleStatusesViewModel : ViewModelBase
    {
        IVehicleStatusesRepository _repository;
        ObservableCollection<IVehicleStatus?>? _list;

        public ObservableCollection<IVehicleStatus?>? List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged(nameof(List));
            }
        }

        public RelayCommand LoadCommand { get; }
        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }

        public VehicleStatusesViewModel()
        {
            _repository = new SQLiteVehicleStatusesRepository();

            LoadCommand = new RelayCommand(ExecuteLoadCommand);
            AddCommand = new RelayCommand(ExecuteAddCommand);
            EditCommand = new RelayCommand(ExecuteEditCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
        }

        private async void ExecuteDeleteCommand(object? obj)
        {
            var entity = obj as VehicleStatus;
            if (entity != null)
            {
                if (new DeleteDialogWindow().ShowDialog() == true)
                {
                    await _repository.Delete(entity);
                    List?.Remove(entity);
                }
            }
        }

        private async void ExecuteEditCommand(object? obj)
        {
            var entity = obj as VehicleStatus;
            if (entity != null)
            {
                if (ShowDialogEditor(entity) == true)
                {
                    await _repository.Edit(entity);
                }
            }
        }

        private async void ExecuteAddCommand(object? obj)
        {
            var entity = new VehicleStatus();
            if (ShowDialogEditor(entity) == true)
            {
                await _repository.Create(entity);
                List?.Add(entity);
            }
        }

        private async void ExecuteLoadCommand(object? obj)
        {
            List = new(await _repository.GetAll());
        }
        private bool? ShowDialogEditor(IVehicleStatus vehicleStatus)
        {
            var dialogViewModel = new VehicleStatusEditorDialogViewModel()
            {
                VehicleStatus = vehicleStatus,
            };

            var dialog = new VehicleStatusEditorDialogView()
            {
                DataContext = dialogViewModel
            };
            return dialog.ShowDialog();
        }
    }
}
