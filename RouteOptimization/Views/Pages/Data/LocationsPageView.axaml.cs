using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using RouteOptimization.Models;
using RouteOptimization.ViewModels.Pages.Data;
using RouteOptimization.ViewModels.Pages.Dialogs;
using RouteOptimization.Views.Pages.Dialogs;
using System;
using System.Threading.Tasks;

namespace RouteOptimization.Views.Pages.Data
{
    public partial class LocationsPageView : ReactiveUserControl<LocationsPageViewModel>
    {
        private Window GetWindow() => TopLevel.GetTopLevel(this) as Window ?? throw new NullReferenceException("Invalid Owner");

        public LocationsPageView()
        {
            InitializeComponent();
            this.WhenActivated(action =>
                action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
        }
        private async Task DoShowDialogAsync(InteractionContext<LocationsDialogViewModel,
                                                Models.Location?> interaction)
        {
            var dialog = new LocationsDialogView();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<Models.Location?>(GetWindow());
            interaction.SetOutput(result);
        }
    }
}
