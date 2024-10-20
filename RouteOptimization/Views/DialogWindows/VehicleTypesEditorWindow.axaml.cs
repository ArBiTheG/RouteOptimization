using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using RouteOptimization.ViewModels.Pages.DataEditors;
using System;

namespace RouteOptimization.Views.DialogWindows
{
    public partial class VehicleTypesEditorWindow : ReactiveWindow<VehicleTypesEditorViewModel>
    {
        public VehicleTypesEditorWindow()
        {
            InitializeComponent();
            // This line is needed to make the previewer happy (the previewer plugin cannot handle the following line).
            if (Design.IsDesignMode) return;

            this.WhenActivated(action => action(ViewModel!.ApplyCommand.Subscribe(Close)));

        }
    }
}
