﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stopify.Domain;
using Stopify.Infrastructure;
using Stopify.Presentation.Utilities.Stores;
using Stopify.Presentation.ViewModels.Home;
using Stopify.Presentation.Views.Main;
using System.IO;
using System.Windows;

namespace Stopify.Presentation;

public partial class App : Application
{
    private IConfiguration _configuration;

    private ServiceProvider _services;
    public ServiceProvider Services { get { return _services; } }

    public App()
    {
        var builder = new ConfigurationBuilder()
                    .SetBasePath(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\")))
                    .AddJsonFile("appsettings.json", false, true);

        _configuration = builder.Build();

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        _services = serviceCollection.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddInfrastructureDependencies(_configuration);
        services.AddDomainDependencies();
        services.AddPresentationDependencies();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        NavigationStore navigationStore = _services.GetService<NavigationStore>()!;
        navigationStore.CurrentViewModel = _services.GetService<HomeViewModel>()!;

        var mainWindow = _services.GetService<MainView>()!;
        mainWindow.Show();

        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _services?.Dispose();
        base.OnExit(e);
    }
}
