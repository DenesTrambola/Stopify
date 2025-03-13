using Microsoft.Extensions.DependencyInjection;
using Stopify.Presentation.ViewModels.Artist;
using Stopify.Presentation.ViewModels.Home;
using Stopify.Presentation.ViewModels.Main;
using Stopify.Presentation.ViewModels.NowPlaying;
using Stopify.Presentation.ViewModels.Playlist;
using Stopify.Presentation.ViewModels.Search;
using Stopify.Presentation.Views.Artist;
using Stopify.Presentation.Views.Home;
using Stopify.Presentation.Views.Main;
using Stopify.Presentation.Views.NowPlaying;
using Stopify.Presentation.Views.Playlist;
using Stopify.Presentation.Views.Search;

namespace Stopify.Presentation;

public static class PresentationDependencies
{
    public static void AddViewDependencies(this IServiceCollection services)
    {
        services.AddSingleton<MainView>()
            .AddTransient<HomeView>()
            .AddTransient<SearchView>()
            .AddTransient<ArtistView>()
            .AddTransient<PlaylistView>()
            .AddTransient<NowPlayingView>();
    }

    public static void AddViewModelDependencies(this IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>()
            .AddTransient<HomeViewModel>()
            .AddTransient<SearchViewModel>()
            .AddTransient<ArtistViewModel>()
            .AddTransient<PlaylistViewModel>()
            .AddTransient<NowPlayingViewModel>();
    }
}
