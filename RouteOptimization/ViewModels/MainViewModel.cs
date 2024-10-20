using Avalonia.Controls;
using ReactiveUI;
using RouteOptimization.ViewModels.Pages;
using RouteOptimization.Views.Pages;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection.Metadata;

namespace RouteOptimization.ViewModels;

public class MainViewModel : ViewModelBase
{
    private PageItem[] PageItems { get; } =
    {
            new PageItem("Home", typeof(HomeViewModel)),
            new PageItem("Handle", typeof(DatabaseViewModel)),
            new PageItem("Builder", typeof(MapBuilderViewModel)),
            new PageItem("Route", typeof(MapRouteViewModel)),
    };

    private bool _isPaneOpen = false;
    private ViewModelBase? _currentPage;
    private HistoryRouter<ViewModelBase> _router;

    public bool IsPaneOpen
    {
        get => _isPaneOpen;
        set => this.RaiseAndSetIfChanged(ref _isPaneOpen, value);
    }


    public ReactiveCommand<Unit, Unit> PaneOpenCloseCommand { get; }
    public ReactiveCommand<string, Unit> OpenPage { get; }

    public ViewModelBase? CurrentPage
    {
        get => _currentPage;
        set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }


    public MainViewModel(HistoryRouter<ViewModelBase> router)
    {
        _router = router;
        _router.CurrentViewModelChanged += (viewModel) => CurrentPage = viewModel;

        _router.GoTo(typeof(HomeViewModel));

        PaneOpenCloseCommand = ReactiveCommand.Create(ExecutePaneOpenCloseCommand);
        OpenPage = ReactiveCommand.Create<string>(ExecuteOpenPage);
    }

    private void ExecutePaneOpenCloseCommand()
    {
        IsPaneOpen = !_isPaneOpen;
    }

    private void ExecuteOpenPage(string pageName)
    {
        foreach (var item in PageItems)
        {
            if (item.Name == pageName)
            {
                _router.GoTo(item.ModelType);
            }
        }
    }
}

