using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using RouteOptimization.ViewModels;
using RouteOptimization.ViewModels.Pages;
using RouteOptimization.ViewModels.Pages.DataViewers;
using RouteOptimization.Views;
using System;

namespace RouteOptimization;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        IServiceProvider services = ConfigureServices();
        var mainViewModel = services.GetRequiredService<MainViewModel>();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = mainViewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        // Add the HistoryRouter as a service
        services.AddSingleton<HistoryRouter<ViewModelBase>>(s => new HistoryRouter<ViewModelBase>(t => (ViewModelBase)s.GetRequiredService(t)));

        // Add the ViewModels as a service (Main as singleton, others as transient)
        services.AddSingleton<MainViewModel>();

        services.AddTransient<HomeViewModel>();
        services.AddTransient<DatabaseViewModel>();
        services.AddTransient<MapBuilderViewModel>();
        services.AddTransient<MapRouteViewModel>();

        services.AddTransient<LocationsViewModel>();
        services.AddTransient<RoutesViewModel>();
        services.AddTransient<ShipmentsViewModel>();
        services.AddTransient<VehiclesViewModel>();
        services.AddTransient<VehicleStatusesViewModel>();
        services.AddTransient<VehicleTypesViewModel>();

        return services.BuildServiceProvider();
    }
}
