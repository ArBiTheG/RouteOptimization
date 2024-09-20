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
    public class VehicleTypesViewModel : ViewModelBase
    {
        IVehicleTypesRepository _repository;
        ObservableCollection<IVehicleType?>? _list;

        public ObservableCollection<IVehicleType?>? List
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

        public VehicleTypesViewModel()
        {
            _repository = new SQLiteVehicleTypesRepository();

            LoadCommand = new RelayCommand(ExecuteLoadCommand);
            AddCommand = new RelayCommand(ExecuteAddCommand);
            EditCommand = new RelayCommand(ExecuteEditCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
        }

        private async void ExecuteDeleteCommand(object? obj)
        {
            var entity = obj as VehicleType;
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
            var entity = obj as VehicleType;
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
            var entity = new VehicleType();
            if (ShowDialogEditor(entity) == true)
            {
                await _repository.Create(entity);
                List?.Add(entity);
            }
        }

        private async void ExecuteLoadCommand(object? obj)
        {
            List = new (await _repository.GetAll());
        }
        private bool? ShowDialogEditor(IVehicleType vehicleType)
        {
            var dialogViewModel = new VehicleTypeEditorDialogViewModel()
            {
                VehicleType = vehicleType,
            };

            var dialog = new VehicleTypeEditorDialogView()
            {
                DataContext = dialogViewModel
            };
            return dialog.ShowDialog();
        }
    }
}
