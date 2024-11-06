using Microsoft.Extensions.DependencyInjection;
using Stopify.Domain.Contracts.Other;
using Stopify.Domain.Contracts.Services;
using Stopify.Domain.Other;
using Stopify.Domain.Services;

namespace Stopify.Domain;

public static class DomainDependencies
{
    public static void AddDomainDependencies(this IServiceCollection services)
    {
        services
            .AddSingleton<IAlbumArtistService, AlbumArtistService>()
            .AddSingleton<IAlbumService, AlbumService>()
            .AddSingleton<IArtistService, ArtistService>()
            .AddSingleton<ICountryService, CountryService>()
            .AddSingleton<IGenreService, GenreService>()
            .AddSingleton<IPlaybackHistoryService, PlaybackHistoryService>()
            .AddSingleton<IPlaylistService, PlaylistService>()
            .AddSingleton<IQueueService, QueueService>()
            .AddSingleton<IRecentPlayedService, RecentPlayedService>()
            .AddSingleton<ISongAlbumService, SongAlbumService>()
            .AddSingleton<ISongArtistService, SongArtistService>()
            .AddSingleton<ISongGenreService, SongGenreService>()
            .AddSingleton<ISongPlaylistService, SongPlaylistService>()
            .AddSingleton<ISongService, SongService>()
            .AddSingleton<IUserAlbumService, UserAlbumService>()
            .AddSingleton<IUserArtistService, UserArtistService>()
            .AddSingleton<IUserFollowerService, UserFollowerService>()
            .AddSingleton<IUserPlaylistService, UserPlaylistService>()
            .AddSingleton<IUserService, UserService>()
            .AddSingleton<IEmailService, EmailService>()
            .AddSingleton<IAudioStorageService, AudioStorageService>();
    }
}
