using Microsoft.Extensions.DependencyInjection;
using Stopify.Presentation.Utilities.Stores;
using Stopify.Presentation.ViewModels.Artist;
using Stopify.Presentation.ViewModels.Home;
using Stopify.Presentation.ViewModels.Main;
using Stopify.Presentation.ViewModels.Playlist;
using Stopify.Presentation.ViewModels.Search;
using Stopify.Presentation.Views.Main;

namespace Stopify.Presentation;

public static class PresentationDependencies
{
    public static IServiceCollection AddPresentationDependencies(this IServiceCollection services)
    {
        services.AddViewDependencies()
                .AddViewModelDependencies()
                .AddStateDependencies();

        return services;
    }

    public static IServiceCollection AddStateDependencies(this IServiceCollection services)
    {
        services.AddSingleton<NavigationStore>()
                .AddSingleton<UIState>();

        return services;
    }

    public static IServiceCollection AddViewModelDependencies(this IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>()
                .AddTransient<HomeViewModel>()
                .AddTransient<SearchViewModel>()
                .AddTransient<ArtistViewModel>()
                .AddTransient<PlaylistViewModel>();

        return services;
    }

    public static IServiceCollection AddViewDependencies(this IServiceCollection services)
    {
        services.AddSingleton<MainView>();

        return services;
    }
}
