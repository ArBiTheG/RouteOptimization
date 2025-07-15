using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using RouteOptimization.ViewModels.Pages.DataEditors;
using RouteOptimization.ViewModels.Pages.DataViewers;
using RouteOptimization.Views.DialogWindows;
using System;
using System.Threading.Tasks;
using Location = RouteOptimization.Models.Entities.Location;

namespace RouteOptimization.Views.Pages.DataViewers
{
    public partial class LocationsView : ReactiveUserControl<LocationsViewModel>
    {
        private Window GetWindow() => TopLevel.GetTopLevel(this) as Window ?? throw new NullReferenceException("Invalid Owner");

        public LocationsView()
        {
            InitializeComponent();
            this.WhenActivated(action =>
                action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
            this.WhenActivated(action =>
                action(ViewModel!.ShowDeleteDialog.RegisterHandler(DoShowDeleteDialogAsync)));
        }


        private async Task DoShowDialogAsync(IInteractionContext<LocationsEditorViewModel,Location?> interaction)
        {
            var dialog = new LocationsEditorWindow();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<Location?>(GetWindow());
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
