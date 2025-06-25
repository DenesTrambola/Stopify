using Microsoft.Extensions.DependencyInjection;
using Stopify.Presentation.Utilities.Stores;
using Stopify.Presentation.ViewModels.Artist;
using Stopify.Presentation.ViewModels.Home;
using Stopify.Presentation.ViewModels.Main;
using Stopify.Presentation.ViewModels.NowPlaying;
using Stopify.Presentation.ViewModels.Player;
using Stopify.Presentation.ViewModels.Playlist;
using Stopify.Presentation.ViewModels.Queue;
using Stopify.Presentation.ViewModels.Search;
using Stopify.Presentation.ViewModels.Sidebar;
using Stopify.Presentation.ViewModels.Titlebar;
using Stopify.Presentation.Views.Artist;
using Stopify.Presentation.Views.Home;
using Stopify.Presentation.Views.Main;
using Stopify.Presentation.Views.NowPlaying;
using Stopify.Presentation.Views.Player;
using Stopify.Presentation.Views.Playlist;
using Stopify.Presentation.Views.Queue;
using Stopify.Presentation.Views.Search;
using Stopify.Presentation.Views.Sidebar;
using Stopify.Presentation.Views.Titlebar;

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
                .AddTransient<PlaylistViewModel>()
                .AddTransient<SidebarViewModel>()
                .AddTransient<NowPlayingViewModel>()
                .AddTransient<QueueViewModel>()
                .AddTransient<TitlebarViewModel>()
                .AddTransient<PlayerViewModel>();

        return services;
    }

    public static IServiceCollection AddViewDependencies(this IServiceCollection services)
    {
        services.AddSingleton<MainView>()
                .AddTransient<HomeView>()
                .AddTransient<SearchView>()
                .AddTransient<ArtistView>()
                .AddTransient<PlaylistView>()
                .AddTransient<SidebarControl>()
                .AddTransient<NowPlayingView>()
                .AddTransient<QueueView>()
                .AddTransient<TitlebarControl>()
                .AddTransient<PlayerControl>();

        return services;
    }
}
