using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using RouteOptimization.Models.Entities;
using RouteOptimization.ViewModels.Pages;
using RouteOptimization.ViewModels.Pages.DataEditors;
using RouteOptimization.Views.DialogWindows;
using System;
using System.Threading.Tasks;

namespace RouteOptimization.Views.Pages
{
    public partial class WarehouseView : ReactiveUserControl<WarehouseViewModel>
    {
        private Window GetWindow() => TopLevel.GetTopLevel(this) as Window ?? throw new NullReferenceException("Invalid Owner");

        public WarehouseView()
        {
            InitializeComponent();
            this.WhenActivated(action =>
                action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
            this.WhenActivated(action =>
                action(ViewModel!.ShowDeleteDialog.RegisterHandler(DoShowDeleteDialogAsync)));
        }

        private async Task DoShowDialogAsync(IInteractionContext<WarehouseEditorViewModel, Cargo?> interaction)
        {
            var dialog = new WarehouseEditorWindow();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<Cargo?>(GetWindow());
            interaction.SetOutput(result);
        }

        private async Task DoShowDeleteDialogAsync(IInteractionContext<DeleteViewModel, bool> interaction)
        {
            var dialog = new DeleteWindow();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<bool>(GetWindow());
            interaction.SetOutput(result);
        }
    }
}
