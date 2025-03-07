using Microsoft.Extensions.DependencyInjection;
using Stopify.Presentation.ViewModels.Artist;
using Stopify.Presentation.ViewModels.Home;
using Stopify.Presentation.ViewModels.Main;
using Stopify.Presentation.ViewModels.Search;
using Stopify.Presentation.Views;
using Stopify.Presentation.Views.HomeView;
using Stopify.Presentation.Views.SearchView;

namespace Stopify.Presentation;

public static class PresentationDependencies
{
    public static void AddViewDependencies(this IServiceCollection services)
    {
        services.AddSingleton<MainWindow>()
            .AddTransient<HomeView>()
            .AddTransient<SearchView>()
            .AddTransient<ArtistView>();
    }

    public static void AddViewModelDependencies(this IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>()
            .AddTransient<HomeViewModel>()
            .AddTransient<SearchViewModel>()
            .AddTransient<ArtistViewModel>();
    }
}
