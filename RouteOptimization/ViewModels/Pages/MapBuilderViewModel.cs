using Avalonia.Metadata;
using Mapsui;
using Mapsui.Layers;
using ReactiveUI;
using RouteOptimization.Controls;
using RouteOptimization.Models;
using RouteOptimization.Models.Entities;
using RouteOptimization.Repository;
using RouteOptimization.Repository.SQLite;
using RouteOptimization.ViewModels.Pages.DataEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages
{
    public class MapBuilderViewModel: ViewModelBase
    {
        private MapBuilderModel Model { get; set; }
        private Map? _map;

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public Interaction<DeleteViewModel, bool> ShowDeleteDialog { get; }

        public Map? Map
        {
            get => _map;
            set => this.RaiseAndSetIfChanged(ref _map, value);
        }

        public MapBuilderViewModel()
        {
            Model = new MapBuilderModel();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ShowDeleteDialog = new Interaction<DeleteViewModel, bool>();
        }
        private async Task ExecuteLoadCommand()
        {
            Map = await Model.GetMap();
        }

    }
}
