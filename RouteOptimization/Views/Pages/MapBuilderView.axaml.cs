using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using RouteOptimization.ViewModels.Pages;
using RouteOptimization.ViewModels.Pages.DataEditors;
using RouteOptimization.Views.DialogWindows;
using System;
using System.Threading.Tasks;

namespace RouteOptimization.Views.Pages
{
    public partial class MapBuilderView : ReactiveUserControl<MapBuilderViewModel>
    {
        private Window GetWindow() => TopLevel.GetTopLevel(this) as Window ?? throw new NullReferenceException("Invalid Owner");

        public MapBuilderView()
        {
            InitializeComponent();

            this.WhenActivated(action =>
                action(ViewModel!.ShowDeleteDialog.RegisterHandler(DoShowDeleteDialogAsync)));
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
