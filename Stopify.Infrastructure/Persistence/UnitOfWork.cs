using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Contracts.Repositories;

namespace Stopify.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly StopifyDbContext _context;

    public IAlbumRepository Albums { get; private set; }
    public IArtistRepository Artists { get; private set; }
    public ICountryRepository Countries { get; private set; }
    public IGenreRepository Genres { get; private set; }
    public IPlaybackHistoryRepository PlaybackHistories { get; private set; }
    public IPlaylistRepository Playlists { get; private set; }
    public IQueueRepository Queues { get; private set; }
    public IRecentPlayedRepository RecentPlays { get; private set; }
    public ISongPlaylistRepository SongPlaylists { get; private set; }
    public ISongRepository Songs { get; private set; }
    public IUserAlbumRepository UserAlbums { get; private set; }
    public IUserArtistRepository UserArtists { get; private set; }
    public IUserPlaylistRepository UserPlaylists { get; private set; }
    public IUserFollowerRepository UserFollowers { get; private set; }
    public IUserRepository Users { get; private set; }

    public UnitOfWork(
        StopifyDbContext context,
        IAlbumRepository albums,
        IArtistRepository artists,
        ICountryRepository countries,
        IGenreRepository genres,
        IPlaybackHistoryRepository playbackHistories,
        IPlaylistRepository playlists,
        IQueueRepository queues,
        IRecentPlayedRepository recentPlays,
        ISongPlaylistRepository songPlaylists,
        ISongRepository songs,
        IUserAlbumRepository userAlbums,
        IUserArtistRepository userArtists,
        IUserPlaylistRepository userPlaylists,
        IUserFollowerRepository userFollowers,
        IUserRepository users
    )
    {
        _context = context;
        Albums = albums;
        Artists = artists;
        Countries = countries;
        Genres = genres;
        PlaybackHistories = playbackHistories;
        Playlists = playlists;
        Queues = queues;
        RecentPlays = recentPlays;
        SongPlaylists = songPlaylists;
        Songs = songs;
        UserAlbums = userAlbums;
        UserArtists = userArtists;
        UserPlaylists = userPlaylists;
        UserFollowers = userFollowers;
        Users = users;
    }

    public async Task<int> SaveChangesAsync() =>
        await _context.SaveChangesAsync();

    public int SaveChanges() =>
        _context.SaveChanges();

    public void Dispose() =>
        _context.Dispose();
}
