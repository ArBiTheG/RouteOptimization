using Avalonia.Metadata;
using ReactiveUI;
using RouteOptimization.Controls.MapBuilder;
using RouteOptimization.Models;
using RouteOptimization.Repository;
using RouteOptimization.Repository.SQLite;
using RouteOptimization.ViewModels.Pages.DataEditors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages
{
    public class MapBuilderViewModel: ViewModelBase
    {
        private static Scene _scene = new Scene(0, 0);

        ObservableCollection<Location?>? _vertices;
        ILocationsRepository _repository;

        public Scene Scene { get => _scene; }

        public ObservableCollection<Location?>? Vertices
        {
            get => _vertices;
            set => this.RaiseAndSetIfChanged(ref _vertices, value);
        }
        public ObservableCollection<Edge> Edges
        {
            get;
            set;
        }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<Location, Unit> EditCommand { get; }
        public ReactiveCommand<Location, Unit> DeleteCommand { get; }

        public MapBuilderViewModel()
        {
            _repository = new SQLiteLocationsRepository();


            Vertices = new();
            Edges = new();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            AddCommand = ReactiveCommand.CreateFromTask(ExecuteAddCommand);
            EditCommand = ReactiveCommand.CreateFromTask<Location>(ExecuteEditCommand);
            DeleteCommand = ReactiveCommand.CreateFromTask<Location>(ExecuteDeleteCommand);
        }

        private async Task ExecuteLoadCommand()
        {
            Vertices = new(await _repository.GetAll());
        }

        private async Task ExecuteAddCommand()
        {
            Location location = new Location()
            {
                Name = "Новая точка",
                X = 0,
                Y = 0,
            };
            await _repository.Create(location);
            Vertices?.Add(location);
        }

        private async Task ExecuteEditCommand(Location location)
        {

        }
        private async Task ExecuteDeleteCommand(Location location)
        {
            await _repository.Delete(location);
            Vertices?.Remove(location);
        }
    }
}
