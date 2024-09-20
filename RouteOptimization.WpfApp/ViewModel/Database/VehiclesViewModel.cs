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
    public class VehiclesViewModel : ViewModelBase
    {
        IVehiclesRepository _repository;
        ObservableCollection<IVehicle?>? _list;

        public ObservableCollection<IVehicle?>? List
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

        public VehiclesViewModel()
        {
            _repository = new SQLiteVehiclesRepository();

            LoadCommand = new RelayCommand(ExecuteLoadCommand);
            AddCommand = new RelayCommand(ExecuteAddCommand);
            EditCommand = new RelayCommand(ExecuteEditCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
        }

        private async void ExecuteDeleteCommand(object? obj)
        {
            var entity = obj as Vehicle;
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
            var entity = obj as Vehicle;
            if (entity != null)
            {
                if (ShowDialogEditor(entity)==true)
                {
                    await _repository.Edit(entity);
                }
            }
        }

        private async void ExecuteAddCommand(object? obj)
        {
            var entity = new Vehicle();
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
        private bool? ShowDialogEditor(IVehicle vehicle)
        {
            var dialogViewModel = new VehicleEditorDialogViewModel()
            {
                Vehicle = vehicle,
            };

            var dialog = new VehicleEditorDialogView()
            {
                DataContext = dialogViewModel
            };
            return dialog.ShowDialog();
        }
    }
}
