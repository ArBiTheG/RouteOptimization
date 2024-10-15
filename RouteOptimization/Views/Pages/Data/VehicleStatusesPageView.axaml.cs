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
    public partial class VehicleStatusesPageView : ReactiveUserControl<VehicleStatusesPageViewModel>
    {
        private Window GetWindow() => TopLevel.GetTopLevel(this) as Window ?? throw new NullReferenceException("Invalid Owner");

        public VehicleStatusesPageView()
        {
            InitializeComponent();
            this.WhenActivated(action =>
                action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
        }
        private async Task DoShowDialogAsync(InteractionContext<VehicleStatusesDialogViewModel,
                                                IVehicleStatus?> interaction)
        {
            var dialog = new VehicleStatusesDialogView();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<IVehicleStatus?>(GetWindow());
            interaction.SetOutput(result);
        }
    }
}
