using Microsoft.Extensions.DependencyInjection;
using Stopify.Presentation.ViewModels.Home;
using Stopify.Presentation.ViewModels.Main;
using Stopify.Presentation.Views.HomeView;

namespace Stopify.Presentation;

public static class PresentationDependencies
{
    public static void AddViewModelDependencies(this IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>()
            .AddTransient<HomeViewModel>();
    }

    public static void AddViewDependencies(this IServiceCollection services)
    {
        services.AddTransient<MainWindow>()
            .AddTransient<HomeView>();
    }
}
