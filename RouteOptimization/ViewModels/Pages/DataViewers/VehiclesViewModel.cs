﻿using ReactiveUI;
using RouteOptimization.Models;
using RouteOptimization.Models.Entities;
using RouteOptimization.ViewModels.Pages.DataEditors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages.DataViewers
{
    public class VehiclesViewModel : ViewModelBase
    {
        VehiclesModel _model;
        ObservableCollection<Vehicle?>? _list;

        public ObservableCollection<Vehicle?>? List
        {
            get => _list;
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }

        public Interaction<VehiclesEditorViewModel, Vehicle?> ShowDialog { get; }
        public Interaction<DeleteViewModel, bool> ShowDeleteDialog { get; }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<Vehicle?, Unit> EditCommand { get; }
        public ReactiveCommand<Vehicle?, Unit> DeleteCommand { get; }

        public VehiclesViewModel()
        {
            ShowDialog = new Interaction<VehiclesEditorViewModel, Vehicle?>();
            ShowDeleteDialog = new Interaction<DeleteViewModel, bool>();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            AddCommand = ReactiveCommand.CreateFromTask(ExecuteAddCommand);
            EditCommand = ReactiveCommand.CreateFromTask<Vehicle?>(ExecuteEditCommand);
            DeleteCommand = ReactiveCommand.CreateFromTask<Vehicle?>(ExecuteDeleteCommand);
        }
        public VehiclesViewModel(VehiclesModel model):this()
        {
            _model = model;
        }

        private async Task ExecuteLoadCommand()
        {
            List = new ObservableCollection<Vehicle?>(await _model.GetAll());
        }

        private async Task ExecuteAddCommand()
        {
            var dialog = new VehiclesEditorViewModel(_model);

            var result = await ShowDialog.Handle(dialog);
            if (result != null)
            {
                await _model.Create(result);
                List?.Add(result);
            }
        }
        private async Task ExecuteEditCommand(Vehicle? vehicle)
        {
            if (vehicle == null) return;

            var dialog = new VehiclesEditorViewModel(_model, vehicle.Clone());

            var result = await ShowDialog.Handle(dialog);
            if (result != null)
            {
                vehicle.CopyFrom(result);
                await _model.Edit(vehicle);
            }
        }
        private async Task ExecuteDeleteCommand(Vehicle? vehicle)
        {
            if (vehicle == null) return;

            var dialog = new DeleteViewModel();

            var result = await ShowDeleteDialog.Handle(dialog);
            if (result != false)
            {
                await _model.Delete(vehicle);
                List?.Remove(vehicle);
            }
        }
    }
}
