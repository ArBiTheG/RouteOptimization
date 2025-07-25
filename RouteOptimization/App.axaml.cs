﻿using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using RouteOptimization.Models;
using RouteOptimization.Models.Entities;
using RouteOptimization.Repository;
using RouteOptimization.Repository.SQLite;
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

        services.AddSingleton<IRepository<Location>, SQLiteLocationsRepository>();
        services.AddSingleton<IRoutesRepository, SQLiteRoutesRepository>();
        services.AddSingleton<ICargosRepository, SQLiteCargosRepository>();
        services.AddSingleton<IRepository<CargoAvailable>, SQLiteCargoAvailablesRepository>();
        services.AddSingleton<IShipmentsRepository, SQLiteShipmentsRepository>();
        services.AddSingleton<IRepository<ShipmentStatus>, SQLiteShipmentStatusesRepository>();
        services.AddSingleton<IRepository<Vehicle>, SQLiteVehiclesRepository>();
        services.AddSingleton<IRepository<VehicleStatus>, SQLiteVehicleStatusesRepository>();
        services.AddSingleton<IRepository<VehicleType>, SQLiteVehicleTypesRepository>();

        services.AddTransient<LocationsModel>();
        services.AddTransient<MapBuilderModel>();
        services.AddTransient<MapRouteModel>();
        services.AddTransient<LoadingModel>();
        services.AddTransient<RoutesModel>();
        services.AddTransient<CargosModel>();
        services.AddTransient<CargoAvailablesModel>();
        services.AddTransient<ShipmentsModel>();
        services.AddTransient<ShipmentStatusesModel>();
        services.AddTransient<VehiclesModel>();
        services.AddTransient<VehicleStatusesModel>();
        services.AddTransient<VehicleTypesModel>();
        services.AddTransient<WarehouseModel>();
        services.AddTransient<HandlingModel>();


        services.AddTransient<HomeViewModel>();
        services.AddTransient<DatabaseViewModel>();
        services.AddTransient<MapBuilderViewModel>();
        services.AddTransient<MapRouteViewModel>();
        services.AddTransient<LoadingViewModel>();
        services.AddTransient<HandlingViewModel>();
        services.AddTransient<WarehouseViewModel>();
        services.AddTransient<SettingsDatabaseViewModel>();

        services.AddTransient<LocationsViewModel>();
        services.AddTransient<RoutesViewModel>();
        services.AddTransient<CargosViewModel>();
        services.AddTransient<CargoAvailablesViewModel>();
        services.AddTransient<ShipmentsViewModel>();
        services.AddTransient<ShipmentStatusesViewModel>();
        services.AddTransient<VehiclesViewModel>();
        services.AddTransient<VehicleStatusesViewModel>();
        services.AddTransient<VehicleTypesViewModel>();

        return services.BuildServiceProvider();
    }
}
