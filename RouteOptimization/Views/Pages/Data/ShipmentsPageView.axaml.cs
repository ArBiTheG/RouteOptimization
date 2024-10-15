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
    public partial class ShipmentsPageView : ReactiveUserControl<ShipmentsPageViewModel>
    {
        private Window GetWindow() => TopLevel.GetTopLevel(this) as Window ?? throw new NullReferenceException("Invalid Owner");

        public ShipmentsPageView()
        {
            InitializeComponent();
            this.WhenActivated(action =>
                action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
        }
        private async Task DoShowDialogAsync(InteractionContext<ShipmentsDialogViewModel,
                                                IShipment?> interaction)
        {
            var dialog = new ShipmentsDialogView();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<IShipment?>(GetWindow());
            interaction.SetOutput(result);
        }
    }
}
