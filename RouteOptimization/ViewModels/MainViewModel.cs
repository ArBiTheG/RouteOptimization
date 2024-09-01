using Avalonia.Controls;
using ReactiveUI;
using RouteOptimization.ViewModels.Pages;
using RouteOptimization.Views.Pages;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;

namespace RouteOptimization.ViewModels;

public class MainViewModel : ViewModelBase
{
    private bool _isPaneOpen = false;
    private ViewModelBase _currentPage;
    private ListItemTemplate? _selectedListItem;

    public bool IsPaneOpen
    {
        get => _isPaneOpen;
        set => this.RaiseAndSetIfChanged(ref _isPaneOpen, value);
    }

    public ViewModelBase CurrentPage
    {
        get => _currentPage;
        set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }

    public ObservableCollection<ListItemTemplate> Items { get; } = new ()
    {
        new ListItemTemplate(typeof(HomePageViewModel), "Главная"),
        new ListItemTemplate(typeof(HandleDataPageViewModel), "Данные"),
        new ListItemTemplate(typeof(MapBuilderPageViewModel), "Конструктор карты"),
    };

    public ListItemTemplate? SelectedListItem
    {
        get => _selectedListItem;
        set => this.RaiseAndSetIfChanged(ref _selectedListItem, value);
    }

    public ReactiveCommand<Unit, Unit> PaneOpenCloseCommand { get; }

    public MainViewModel()
    {
        CurrentPage = new HomePageViewModel();

        this.WhenAnyValue(vm => vm.SelectedListItem).Subscribe(t => UpdateSelectedListItemCommand(_selectedListItem));
        PaneOpenCloseCommand = ReactiveCommand.Create(ExecutePaneOpenCloseCommand);
    }

    private void UpdateSelectedListItemCommand(ListItemTemplate? value)
    {
        if (value is null) return;
        var instance = Activator.CreateInstance(value.ModelType);
        if (instance is null) return;
        CurrentPage = (ViewModelBase)instance;
    }

    private void ExecutePaneOpenCloseCommand()
    {
        IsPaneOpen = !_isPaneOpen;
    }
}

public class ListItemTemplate
{
    public string Title { get;}
    public Type ModelType { get; }
    public ListItemTemplate(Type type, string title)
    {
        ModelType = type;
        Title = title;
    }
}

