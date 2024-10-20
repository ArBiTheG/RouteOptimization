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
    public partial class LocationsView : ReactiveUserControl<LocationsViewModel>
    {
        private Window GetWindow() => TopLevel.GetTopLevel(this) as Window ?? throw new NullReferenceException("Invalid Owner");

        public LocationsView()
        {
            InitializeComponent();
            this.WhenActivated(action =>
                action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
        }
        private async Task DoShowDialogAsync(InteractionContext<LocationsEditorViewModel,
                                                Models.Location?> interaction)
        {
            var dialog = new LocationsEditorWindow();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<Models.Location?>(GetWindow());
            interaction.SetOutput(result);
        }
    }
}
