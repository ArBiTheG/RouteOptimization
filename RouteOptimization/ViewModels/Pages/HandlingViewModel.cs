using ReactiveUI;
using RouteOptimization.Models;
using RouteOptimization.Models.Entities;
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
    public class HandlingViewModel : ViewModelBase
    {
        private HandlingModel _handlingModel;

        private ObservableCollection<Shipment?>? _shipments;
        private Shipment? _currentShipment;

        public ObservableCollection<Shipment?>? Shipments
        {
            get => _shipments;
            set => this.RaiseAndSetIfChanged(ref _shipments, value);
        }

        public Shipment? CurrentShipment
        {
            get => _currentShipment;
            set => this.RaiseAndSetIfChanged(ref _currentShipment, value);
        }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Shipment?, Unit> ApplyCommand { get; }
        public ReactiveCommand<Shipment?, Unit> CancelCommand { get; }

        public HandlingViewModel()
        {
            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            ApplyCommand = ReactiveCommand.CreateFromTask<Shipment?>(ExecuteApplyCommand);
            CancelCommand = ReactiveCommand.CreateFromTask<Shipment?>(ExecuteCancelCommand);
        }

        public HandlingViewModel(HandlingModel handlingModel) : this()
        {
            _handlingModel = handlingModel;
        }

        private async Task ExecuteApplyCommand(Shipment? shipment)
        {
            if (shipment == null) return;
            await _handlingModel.Apply(shipment);
            Shipments?.Remove(shipment);
        }

        private async Task ExecuteCancelCommand(Shipment? shipment)
        {
            if (shipment == null) return;
            await _handlingModel.Cancel(shipment);
            Shipments?.Remove(shipment);
        }

        private async Task ExecuteLoadCommand()
        {
            Shipments = new(await _handlingModel.GetShipments());
        }
    }
}
