using Microsoft.EntityFrameworkCore;
using Stopify.Domain.Contracts.Repositories;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Infrastructure.Persistence.Repositories;

public class UserPlaylistRepository : GenericRepository<UserPlaylist>, IUserPlaylistRepository
{
    private readonly StopifyDbContext _context;

    public UserPlaylistRepository(StopifyDbContext context) : base(context) =>
        _context = context;

    public async Task<IEnumerable<UserPlaylist>?> GetAllByPlaylistIdAsync(int playlistId, Expression<Func<UserPlaylist, bool>>? expression = null) =>
        await _context.UserPlaylists.Where(e => e.PlaylistId == playlistId)
        .Include(e => e.Playlist)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<UserPlaylist>?> GetAllBySavedDateAsync(DateTime savedDate, Expression<Func<UserPlaylist, bool>>? expression = null) =>
        await _context.UserPlaylists.Where(e => e.SavedDate == savedDate)
        .Include(e => e.Playlist)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<UserPlaylist>?> GetAllByUserIdAsync(int userId, Expression<Func<UserPlaylist, bool>>? expression = null) =>
        await _context.UserPlaylists.Where(e => e.UserId == userId)
        .Include(e => e.Playlist)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<UserPlaylist?> GetFirstByPlaylistIdAsync(int playlistId, Expression<Func<UserPlaylist, bool>>? expression = null) =>
        await _context.UserPlaylists.Where(e => e.PlaylistId == playlistId)
        .Include(e => e.Playlist)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<UserPlaylist?> GetFirstBySavedDateAsync(DateTime savedDate, Expression<Func<UserPlaylist, bool>>? expression = null) =>
        await _context.UserPlaylists.Where(e => e.SavedDate == savedDate)
        .Include(e => e.Playlist)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<UserPlaylist?> GetFirstByUserIdAsync(int userId, Expression<Func<UserPlaylist, bool>>? expression = null) =>
        await _context.UserPlaylists.Where(e => e.UserId == userId)
        .Include(e => e.Playlist)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();
}
