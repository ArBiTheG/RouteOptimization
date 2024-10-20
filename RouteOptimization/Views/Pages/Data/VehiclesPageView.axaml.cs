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
    public partial class VehiclesPageView : ReactiveUserControl<VehiclesPageViewModel>
    {
        private Window GetWindow() => TopLevel.GetTopLevel(this) as Window ?? throw new NullReferenceException("Invalid Owner");

        public VehiclesPageView()
        {
            InitializeComponent();
            this.WhenActivated(action =>
                action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
        }
        private async Task DoShowDialogAsync(InteractionContext<VehiclesDialogViewModel,
                                                Vehicle?> interaction)
        {
            var dialog = new VehiclesDialogView();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<Vehicle?>(GetWindow());
            interaction.SetOutput(result);
        }
    }
}
