using Microsoft.Extensions.DependencyInjection;
using Stopify.Presentation.ViewModels.ArtistViewModel;
using Stopify.Presentation.ViewModels.CommonViewModels;
using Stopify.Presentation.ViewModels.HomeViewModel;
using Stopify.Presentation.ViewModels.NowPlayingViewModel;
using Stopify.Presentation.ViewModels.PlayerViewModel;
using Stopify.Presentation.ViewModels.PlaylistViewModel;
using Stopify.Presentation.ViewModels.QueueViewModel;
using Stopify.Presentation.ViewModels.SearchViewModel;
using Stopify.Presentation.ViewModels.SidebarViewModel;
using Stopify.Presentation.ViewModels.TitlebarViewModel;

namespace Stopify.Presentation;

public static class PresentationDependencies
{
    public static void AddPresentationDependencies(this IServiceCollection services)
    {
        services.AddSingleton<MainWindow>()
        .AddSingleton<ArtistViewModel>()
        .AddSingleton<CommonItemViewModel>()
        .AddSingleton<CommonRowViewModel>()
        .AddSingleton<FilterRowViewModel>()
        .AddSingleton<PopupOnHoverViewModel>()
        .AddSingleton<HomeViewModel>()
        .AddSingleton<HomeRecentsViewModel>()
        .AddSingleton<NowPlayingViewModel>()
        .AddSingleton<NowPlayingItemViewModel>()
        .AddSingleton<PlayerViewModel>()
        .AddSingleton<PlaylistViewModel>()
        .AddSingleton<PlaylistItemViewModel>()
        .AddSingleton<PlaylistHeaderViewModel>()
        .AddSingleton<QueueViewModel>()
        .AddSingleton<SearchViewModel>()
        .AddSingleton<SearchItemViewModel>()
        .AddSingleton<SidebarViewModel>()
        .AddSingleton<SidebarItemViewModel>()
        .AddSingleton<TitlebarViewModel>();
    }
}
