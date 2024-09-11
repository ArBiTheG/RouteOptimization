using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using RouteOptimization.ViewModels;
using RouteOptimization.ViewModels.Pages;
using RouteOptimization.ViewModels.Pages.Data;
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

        services.AddTransient<HomePageViewModel>();
        services.AddTransient<HandleDataPageViewModel>();
        services.AddTransient<MapBuilderPageViewModel>();
        services.AddTransient<MapRoutePageViewModel>();

        services.AddTransient<LocationsPageViewModel>();
        services.AddTransient<RoutesPageViewModel>();
        services.AddTransient<ShipmentsPageViewModel>();
        services.AddTransient<VehiclesPageViewModel>();
        services.AddTransient<VehicleStatusesPageViewModel>();
        services.AddTransient<VehicleTypesPageViewModel>();

        return services.BuildServiceProvider();
    }
}
