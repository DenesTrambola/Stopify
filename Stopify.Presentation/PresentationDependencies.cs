using Microsoft.Extensions.DependencyInjection;
using Stopify.Presentation.ViewModels.Home;
using Stopify.Presentation.ViewModels.Main;
using Stopify.Presentation.ViewModels.Search;
using Stopify.Presentation.Views.HomeView;
using Stopify.Presentation.Views.SearchView;

namespace Stopify.Presentation;

public static class PresentationDependencies
{
    public static void AddViewModelDependencies(this IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>()
            .AddTransient<HomeViewModel>()
            .AddTransient<SearchViewModel>();
    }

    public static void AddViewDependencies(this IServiceCollection services)
    {
        services.AddSingleton<MainWindow>()
            .AddTransient<HomeView>()
            .AddTransient<SearchView>();
    }
}
