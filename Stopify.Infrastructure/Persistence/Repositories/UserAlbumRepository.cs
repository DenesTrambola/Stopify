using Microsoft.EntityFrameworkCore;
using Stopify.Domain.Contracts.Repositories;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Infrastructure.Persistence.Repositories;

public class UserAlbumRepository : GenericRepository<UserAlbum>, IUserAlbumRepository
{
    private readonly StopifyDbContext _context;

    public UserAlbumRepository(StopifyDbContext context) : base(context) =>
        _context = context;

    public async Task<IEnumerable<UserAlbum>?> GetAllByAlbumIdAsync(int albumId, Expression<Func<UserAlbum, bool>>? expression = null) =>
        await _context.UserAlbums.Where(e => e.AlbumId == albumId)
        .Include(e => e.Album)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<UserAlbum>?> GetAllBySavedDateAsync(DateTime savedDate, Expression<Func<UserAlbum, bool>>? expression = null) =>
        await _context.UserAlbums.Where(e => e.SavedDate == savedDate)
        .Include(e => e.Album)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<UserAlbum>?> GetAllByUserIdAsync(int userId, Expression<Func<UserAlbum, bool>>? expression = null) =>
        await _context.UserAlbums.Where(e => e.UserId == userId)
        .Include(e => e.Album)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<UserAlbum?> GetFirstByAlbumIdAsync(int albumId, Expression<Func<UserAlbum, bool>>? expression = null) =>
        await _context.UserAlbums.Where(e => e.AlbumId == albumId)
        .Include(e => e.Album)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<UserAlbum?> GetFirstBySaveDateAsync(DateTime savedDate, Expression<Func<UserAlbum, bool>>? expression = null) =>
        await _context.UserAlbums.Where(e => e.SavedDate == savedDate)
        .Include(e => e.Album)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<UserAlbum?> GetFirstByUserIdAsync(int userId, Expression<Func<UserAlbum, bool>>? expression = null) =>
        await _context.UserAlbums.Where(e => e.UserId == userId)
        .Include(e => e.Album)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();
}
