using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using RouteOptimization.Models;
using RouteOptimization.ViewModels.Pages.DataEditors;
using RouteOptimization.ViewModels.Pages.DataViewers;
using RouteOptimization.Views.DialogWindows;
using System;
using System.Threading.Tasks;

namespace RouteOptimization.Views.Pages.DataViewers
{
    public partial class VehicleStatusesView : ReactiveUserControl<VehicleStatusesViewModel>
    {
        private Window GetWindow() => TopLevel.GetTopLevel(this) as Window ?? throw new NullReferenceException("Invalid Owner");

        public VehicleStatusesView()
        {
            InitializeComponent();
            this.WhenActivated(action =>
                action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
            this.WhenActivated(action =>
                action(ViewModel!.ShowDeleteDialog.RegisterHandler(DoShowDeleteDialogAsync)));
        }
        private async Task DoShowDialogAsync(InteractionContext<VehicleStatusesEditorViewModel, VehicleStatus?> interaction)
        {
            var dialog = new VehicleStatusesEditorWindow();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<VehicleStatus?>(GetWindow());
            interaction.SetOutput(result);
        }

        private async Task DoShowDeleteDialogAsync(InteractionContext<DeleteViewModel, bool> interaction)
        {
            var dialog = new DeleteWindow();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<bool>(GetWindow());
            interaction.SetOutput(result);
        }
    }
}
