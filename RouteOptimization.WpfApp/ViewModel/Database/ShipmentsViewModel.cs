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
    public class ShipmentsViewModel : ViewModelBase
    {
        IShipmentsRepository _repository;
        ObservableCollection<IShipment?>? _list;

        public ObservableCollection<IShipment?>? List
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

        public ShipmentsViewModel()
        {
            _repository = new SQLiteShipmentsRepository();

            LoadCommand = new RelayCommand(ExecuteLoadCommand);
            AddCommand = new RelayCommand(ExecuteAddCommand);
            EditCommand = new RelayCommand(ExecuteEditCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
        }

        private async void ExecuteDeleteCommand(object? obj)
        {
            var entity = obj as Shipment;
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
            var entity = obj as Shipment;
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
            var entity = new Shipment();
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
        private bool? ShowDialogEditor(IShipment shipment)
        {
            var dialogViewModel = new ShipmentEditorDialogViewModel()
            {
                Shipment = shipment,
            };

            var dialog = new ShipmentEditorDialogView()
            {
                DataContext = dialogViewModel
            };
            return dialog.ShowDialog();
        }
    }
}
