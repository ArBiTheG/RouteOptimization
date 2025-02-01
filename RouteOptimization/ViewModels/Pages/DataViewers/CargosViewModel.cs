using ReactiveUI;
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
    public class CargosViewModel : ViewModelBase
    {
        CargosModel _model;
        ObservableCollection<Cargo?>? _list;

        public ObservableCollection<Cargo?>? List
        {
            get => _list;
            set => this.RaiseAndSetIfChanged(ref _list, value);
        }

        public Interaction<CargosEditorViewModel, Cargo?> ShowDialog { get; }
        public Interaction<DeleteViewModel, bool> ShowDeleteDialog { get; }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<Cargo?, Unit> EditCommand { get; }
        public ReactiveCommand<Cargo?, Unit> DeleteCommand { get; }

        public CargosViewModel()
        {
            ShowDialog = new Interaction<CargosEditorViewModel, Cargo?>();
            ShowDeleteDialog = new Interaction<DeleteViewModel, bool>();

            LoadCommand = ReactiveCommand.CreateFromTask(ExecuteLoadCommand);
            AddCommand = ReactiveCommand.CreateFromTask(ExecuteAddCommand);
            EditCommand = ReactiveCommand.CreateFromTask<Cargo?>(ExecuteEditCommand);
            DeleteCommand = ReactiveCommand.CreateFromTask<Cargo?>(ExecuteDeleteCommand);
        }
        public CargosViewModel(CargosModel model) : this()
        {
            _model = model;
        }

        private async Task ExecuteLoadCommand()
        {
            List = new(await _model.GetAll());
        }

        private async Task ExecuteAddCommand()
        {
            var dialog = new CargosEditorViewModel(_model);

            var result = await ShowDialog.Handle(dialog);
            if (result != null)
            {
                await _model.Create(result);
                List?.Add(result);
            }
        }
        private async Task ExecuteEditCommand(Cargo? cargo)
        {
            if (cargo == null) return;
            var dialog = new CargosEditorViewModel(_model, cargo.Clone());

            var result = await ShowDialog.Handle(dialog);
            if (result != null)
            {
                cargo.CopyFrom(result);
                await _model.Edit(cargo);
            }
        }
        private async Task ExecuteDeleteCommand(Cargo? cargo)
        {
            if (cargo == null) return;

            var dialog = new DeleteViewModel();

            var result = await ShowDeleteDialog.Handle(dialog);
            if (result != false)
            {
                await _model.Delete(cargo);
                List?.Remove(cargo);
            }
        }
    }
}
