using RouteOptimization.WpfApp.Models;
using RouteOptimization.WpfApp.Repository;
using RouteOptimization.WpfApp.Repository.SQLite;
using RouteOptimization.WpfApp.Utilties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.ViewModel.Database
{
    public class LocationsViewModel : ViewModelBase
    {
        ILocationsRepository _repository;
        ObservableCollection<ILocation?>? _list;

        public ObservableCollection<ILocation?>? List
        {
            get => _list;
            set{
                _list = value;
                OnPropertyChanged(nameof(List));
            }
        }

        public RelayCommand LoadCommand { get; }
        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }

        public LocationsViewModel()
        {
            _repository = new SQLiteLocationsRepository();

            LoadCommand = new RelayCommand(ExecuteLoadCommand);
            AddCommand = new RelayCommand(ExecuteAddCommand);
            EditCommand = new RelayCommand(ExecuteEditCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
        }

        private void ExecuteDeleteCommand(object? obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteEditCommand(object? obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteAddCommand(object? obj)
        {
            throw new NotImplementedException();
        }

        private async void ExecuteLoadCommand(object? obj)
        {
            List = new (await _repository.GetAll());
        }
    }
}
