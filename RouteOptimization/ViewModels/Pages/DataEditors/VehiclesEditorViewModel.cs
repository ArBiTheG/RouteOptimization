﻿using ReactiveUI;
using RouteOptimization.Models;
using RouteOptimization.Models.Entities;
using RouteOptimization.Repository;
using RouteOptimization.Repository.SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages.DataEditors
{
    public class VehiclesEditorViewModel : ViewModelBase
    {
        Vehicle _selectedVehicle;
        VehiclesModel _vehiclesModel;
        ObservableCollection<VehicleStatus?>? _statuses;
        ObservableCollection<VehicleType?>? _types;

        public Vehicle SelectedVehicle
        {
            get => _selectedVehicle;
            set => this.RaiseAndSetIfChanged(ref _selectedVehicle, value);
        }
        public ObservableCollection<VehicleStatus?>? Statuses
        {
            get => _statuses;
            set => this.RaiseAndSetIfChanged(ref _statuses, value);
        }
        public ObservableCollection<VehicleType?>? Types
        {
            get => _types;
            set => this.RaiseAndSetIfChanged(ref _types, value);
        }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<bool, Vehicle?> ApplyCommand { get; }

        public VehiclesEditorViewModel()
        {
            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.Create<bool, Vehicle?>(ExecuteApplyCommand);
        }
        public VehiclesEditorViewModel(VehiclesModel model) : this(model, new()) { }
        public VehiclesEditorViewModel(VehiclesModel model, Vehicle vehicle): this()
        {
            _vehiclesModel = model;
            _selectedVehicle = vehicle;
        }


        private async Task ExecuteLoadCommand()
        {
            Types = new(await _vehiclesModel.GetTypes());
            Statuses = new(await _vehiclesModel.GetStatuses());
        }
        private Vehicle? ExecuteApplyCommand(bool arg)
        {
            if (arg)
                return SelectedVehicle;
            return null;
        }
    }
}
