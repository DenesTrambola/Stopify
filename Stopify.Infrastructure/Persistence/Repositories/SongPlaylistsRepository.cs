using Microsoft.EntityFrameworkCore;
using Stopify.Domain.Contracts.Repositories;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Infrastructure.Persistence.Repositories;

public class SongPlaylistsRepository : GenericRepository<SongPlaylist>, ISongPlaylistRepository
{
    private readonly StopifyDbContext _context;

    public SongPlaylistsRepository(StopifyDbContext context) : base(context) =>
        _context = context;

    public async Task<IEnumerable<SongPlaylist>?> GetAllByPlaylistIdAsync(int playlistId, Expression<Func<SongPlaylist, bool>>? expression = null) =>
        await _context.SongPlaylists.Where(e => e.PlaylistId == playlistId)
        .Include(e => e.Playlist)
        .Include(e => e.Song)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<SongPlaylist>?> GetAllByPositionAsync(int position, Expression<Func<SongPlaylist, bool>>? expression = null) =>
        await _context.SongPlaylists.Where(e => e.Position == position)
        .Include(e => e.Playlist)
        .Include(e => e.Song)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<SongPlaylist>?> GetAllBySongIdAsync(int songId, Expression<Func<SongPlaylist, bool>>? expression = null) =>
        await _context.SongPlaylists.Where(e => e.SongId == songId)
        .Include(e => e.Playlist)
        .Include(e => e.Song)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<SongPlaylist?> GetFirstByPlaylistIdAsync(int playlistId, Expression<Func<SongPlaylist, bool>>? expression = null) =>
        await _context.SongPlaylists.Where(e => e.PlaylistId == playlistId)
        .Include(e => e.Playlist)
        .Include(e => e.Song)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<SongPlaylist?> GetFirstByPositionAsync(int position, Expression<Func<SongPlaylist, bool>>? expression = null) =>
        await _context.SongPlaylists.Where(e => e.Position == position)
        .Include(e => e.Playlist)
        .Include(e => e.Song)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<SongPlaylist?> GetFirstBySongIdAsync(int songId, Expression<Func<SongPlaylist, bool>>? expression = null) =>
        await _context.SongPlaylists.Where(e => e.SongId == songId)
        .Include(e => e.Playlist)
        .Include(e => e.Song)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();
}
