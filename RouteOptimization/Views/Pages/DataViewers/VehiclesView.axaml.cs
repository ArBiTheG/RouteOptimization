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
    public partial class VehiclesView : ReactiveUserControl<VehiclesViewModel>
    {
        private Window GetWindow() => TopLevel.GetTopLevel(this) as Window ?? throw new NullReferenceException("Invalid Owner");

        public VehiclesView()
        {
            InitializeComponent();
            this.WhenActivated(action =>
                action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
        }
        private async Task DoShowDialogAsync(InteractionContext<VehiclesEditorViewModel,
                                                Vehicle?> interaction)
        {
            var dialog = new VehiclesEditorWindow();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<Vehicle?>(GetWindow());
            interaction.SetOutput(result);
        }
    }
}
