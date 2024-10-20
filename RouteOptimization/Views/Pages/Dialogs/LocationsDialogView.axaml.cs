using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using RouteOptimization.ViewModels.Pages.Dialogs;
using System;

namespace RouteOptimization.Views.Pages.Dialogs
{
    public partial class LocationsDialogView : ReactiveWindow<LocationsDialogViewModel>
    {
        public LocationsDialogView()
        {
            InitializeComponent();

            // This line is needed to make the previewer happy (the previewer plugin cannot handle the following line).
            if (Design.IsDesignMode) return;

            this.WhenActivated(action => action(ViewModel!.ApplyCommand.Subscribe(Close)));
        }
    }
}
