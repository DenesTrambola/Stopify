using Stopify.Domain.Contracts.Repositories;

namespace Stopify.Domain.Contracts.Common;

public interface IUnitOfWork : IDisposable
{
    IAlbumRepository Albums { get; }
    IArtistRepository Artists { get; }
    ICountryRepository Countries { get; }
    IGenreRepository Genres { get; }
    IPlaybackHistoryRepository PlaybackHistories { get; }
    IPlaylistRepository Playlists { get; }
    IQueueRepository Queues { get; }
    IRecentPlayedRepository RecentPlays { get; }
    ISongPlaylistRepository SongPlaylists { get; }
    ISongRepository Songs { get; }
    IUserAlbumRepository UserAlbums { get; }
    IUserArtistRepository UserArtists { get; }
    IUserPlaylistRepository UserPlaylists { get; }
    IUserFollowerRepository UserFollowers { get; }
    IUserRepository Users { get; }

    Task<int> SaveChangesAsync();
    int SaveChanges();
}
