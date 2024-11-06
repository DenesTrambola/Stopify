using Microsoft.EntityFrameworkCore;
using Stopify.Domain.Contracts.Repositories;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Infrastructure.Persistence.Repositories;

public class SongRepository : GenericEntityRepository<Song>, ISongRepository
{
    private readonly StopifyDbContext _context;

    public SongRepository(StopifyDbContext context) : base(context) =>
        _context = context;

    public async Task<IEnumerable<Song>?> GetAllByAlbumIdAsync(int albumId, Expression<Func<Song, bool>>? expression = null) =>
        await _context.Songs.Where(e => e.AlbumId == albumId)
        .Include(e => e.Album)
        .Include(e => e.Queues)
        .Include(e => e.RecentPlays)
        .Include(e => e.SongPlaylists)
        .Include(e => e.PlaybackHistories)
        .Include(e => e.Artists)
        .Include(e => e.Genres)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<Song>?> GetAllByCoverAsync(string cover, Expression<Func<Song, bool>>? expression = null) =>
        await _context.Songs.Where(e => e.Cover == cover)
        .Include(e => e.Album)
        .Include(e => e.Queues)
        .Include(e => e.RecentPlays)
        .Include(e => e.SongPlaylists)
        .Include(e => e.PlaybackHistories)
        .Include(e => e.Artists)
        .Include(e => e.Genres)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<Song>?> GetAllByDurationAsync(int duration, Expression<Func<Song, bool>>? expression = null) =>
        await _context.Songs.Where(e => e.Duration == duration)
        .Include(e => e.Album)
        .Include(e => e.Queues)
        .Include(e => e.RecentPlays)
        .Include(e => e.SongPlaylists)
        .Include(e => e.PlaybackHistories)
        .Include(e => e.Artists)
        .Include(e => e.Genres)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<Song>?> GetAllByPathAsync(string path, Expression<Func<Song, bool>>? expression = null) =>
        await _context.Songs.Where(e => e.Path == path)
        .Include(e => e.Album)
        .Include(e => e.Queues)
        .Include(e => e.RecentPlays)
        .Include(e => e.SongPlaylists)
        .Include(e => e.PlaybackHistories)
        .Include(e => e.Artists)
        .Include(e => e.Genres)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<Song>?> GetAllByReleaseDateAsync(DateOnly releaseDate, Expression<Func<Song, bool>>? expression = null) =>
        await _context.Songs.Where(e => e.ReleaseDate == releaseDate)
        .Include(e => e.Album)
        .Include(e => e.Queues)
        .Include(e => e.RecentPlays)
        .Include(e => e.SongPlaylists)
        .Include(e => e.PlaybackHistories)
        .Include(e => e.Artists)
        .Include(e => e.Genres)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<Song?> GetFirstByAlbumIdAsync(int albumId, Expression<Func<Song, bool>>? expression = null) =>
        await _context.Songs.Where(e => e.AlbumId == albumId)
        .Include(e => e.Album)
        .Include(e => e.Queues)
        .Include(e => e.RecentPlays)
        .Include(e => e.SongPlaylists)
        .Include(e => e.PlaybackHistories)
        .Include(e => e.Artists)
        .Include(e => e.Genres)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<Song?> GetFirstByCoverAsync(string cover, Expression<Func<Song, bool>>? expression = null) =>
        await _context.Songs.Where(e => e.Cover == cover)
        .Include(e => e.Album)
        .Include(e => e.Queues)
        .Include(e => e.RecentPlays)
        .Include(e => e.SongPlaylists)
        .Include(e => e.PlaybackHistories)
        .Include(e => e.Artists)
        .Include(e => e.Genres)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<Song?> GetFirstByDurationAsync(int duration, Expression<Func<Song, bool>>? expression = null) =>
        await _context.Songs.Where(e => e.Duration == duration)
        .Include(e => e.Album)
        .Include(e => e.Queues)
        .Include(e => e.RecentPlays)
        .Include(e => e.SongPlaylists)
        .Include(e => e.PlaybackHistories)
        .Include(e => e.Artists)
        .Include(e => e.Genres)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<Song?> GetFirstByPathAsync(string path, Expression<Func<Song, bool>>? expression = null) =>
        await _context.Songs.Where(e => e.Path == path)
        .Include(e => e.Album)
        .Include(e => e.Queues)
        .Include(e => e.RecentPlays)
        .Include(e => e.SongPlaylists)
        .Include(e => e.PlaybackHistories)
        .Include(e => e.Artists)
        .Include(e => e.Genres)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<Song?> GetFirstByReleaseDateAsync(DateOnly releaseDate, Expression<Func<Song, bool>>? expression = null) =>
        await _context.Songs.Where(e => e.ReleaseDate == releaseDate)
        .Include(e => e.Album)
        .Include(e => e.Queues)
        .Include(e => e.RecentPlays)
        .Include(e => e.SongPlaylists)
        .Include(e => e.PlaybackHistories)
        .Include(e => e.Artists)
        .Include(e => e.Genres)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<Song?> GetByTitleAsync(string title, Expression<Func<Song, bool>>? expression = null) =>
        await _context.Songs.Where(e => e.Title == title)
        .Include(e => e.Album)
        .Include(e => e.Queues)
        .Include(e => e.RecentPlays)
        .Include(e => e.SongPlaylists)
        .Include(e => e.PlaybackHistories)
        .Include(e => e.Artists)
        .Include(e => e.Genres)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();
}
