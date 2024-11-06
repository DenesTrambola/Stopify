using Microsoft.EntityFrameworkCore;
using Stopify.Domain.Contracts.Repositories;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Infrastructure.Persistence.Repositories;

public class PlaylistRepository : GenericEntityRepository<Playlist>, IPlaylistRepository
{
    private readonly StopifyDbContext _context;

    public PlaylistRepository(StopifyDbContext context) : base(context) =>
        _context = context;

    public async Task<IEnumerable<Playlist>?> GetAllByDescriptionAsync(string description, Expression<Func<Playlist, bool>>? expression = null) =>
        await _context.Playlists.Where(e => e.Description == description)
        .Include(e => e.SongPlaylists)
        .Include(e => e.UserPlaylists)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<Playlist>?> GetAllByDurationAsync(int duration, Expression<Func<Playlist, bool>>? expression = null) =>
        await _context.Playlists.Where(e => e.Duration == duration)
        .Include(e => e.SongPlaylists)
        .Include(e => e.UserPlaylists)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<Playlist>?> GetAllBySavesAsync(int saves, Expression<Func<Playlist, bool>>? expression = null) =>
        await _context.Playlists.Where(e => e.Saves == saves)
        .Include(e => e.SongPlaylists)
        .Include(e => e.UserPlaylists)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<Playlist>?> GetAllBySongsAsync(int songs, Expression<Func<Playlist, bool>>? expression = null) =>
        await _context.Playlists.Where(e => e.Songs == songs)
        .Include(e => e.SongPlaylists)
        .Include(e => e.UserPlaylists)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<Playlist>?> GetAllNotPublicAsync(Expression<Func<Playlist, bool>>? expression = null) =>
        await _context.Playlists.Where(e => !e.IsPublic)
        .Include(e => e.SongPlaylists)
        .Include(e => e.UserPlaylists)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<Playlist>?> GetAllPublicAsync(Expression<Func<Playlist, bool>>? expression = null) =>
        await _context.Playlists.Where(e => e.IsPublic)
        .Include(e => e.SongPlaylists)
        .Include(e => e.UserPlaylists)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<Playlist>?> GetAllByCoverAsync(string cover, Expression<Func<Playlist, bool>>? expression = null) =>
        await _context.Playlists.Where(e => e.Cover == cover)
        .Include(e => e.SongPlaylists)
        .Include(e => e.UserPlaylists)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<Playlist?> GetFirstByDescriptionAsync(string description, Expression<Func<Playlist, bool>>? expression = null) =>
        await _context.Playlists.Where(e => e.Description == description)
        .Include(e => e.SongPlaylists)
        .Include(e => e.UserPlaylists)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<Playlist?> GetFirstByDurationAsync(int duration, Expression<Func<Playlist, bool>>? expression = null) =>
        await _context.Playlists.Where(e => e.Duration == duration)
        .Include(e => e.SongPlaylists)
        .Include(e => e.UserPlaylists)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<Playlist?> GetFirstBySavesAsync(int saves, Expression<Func<Playlist, bool>>? expression = null) =>
        await _context.Playlists.Where(e => e.Saves == saves)
        .Include(e => e.SongPlaylists)
        .Include(e => e.UserPlaylists)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<Playlist?> GetFirstBySongsAsync(int songs, Expression<Func<Playlist, bool>>? expression = null) =>
        await _context.Playlists.Where(e => e.Songs == songs)
        .Include(e => e.SongPlaylists)
        .Include(e => e.UserPlaylists)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<Playlist?> GetByTitleAsync(string title, Expression<Func<Playlist, bool>>? expression = null) =>
        await _context.Playlists.Where(e => e.Title == title)
        .Include(e => e.SongPlaylists)
        .Include(e => e.UserPlaylists)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<Playlist?> GetFirstNotPublicAsync(Expression<Func<Playlist, bool>>? expression = null) =>
        await _context.Playlists.Where(e => !e.IsPublic)
        .Include(e => e.SongPlaylists)
        .Include(e => e.UserPlaylists)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<Playlist?> GetFirstPublicAsync(Expression<Func<Playlist, bool>>? expression = null) =>
        await _context.Playlists.Where(e => e.IsPublic)
        .Include(e => e.SongPlaylists)
        .Include(e => e.UserPlaylists)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<Playlist?> GetFirstByCoverAsync(string cover, Expression<Func<Playlist, bool>>? expression = null) =>
        await _context.Playlists.Where(e => e.Cover == cover)
        .Include(e => e.SongPlaylists)
        .Include(e => e.UserPlaylists)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();
}
