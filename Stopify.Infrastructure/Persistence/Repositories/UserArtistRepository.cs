using Microsoft.EntityFrameworkCore;
using Stopify.Domain.Contracts.Repositories;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Infrastructure.Persistence.Repositories;

public class UserArtistRepository : GenericRepository<UserArtist>, IUserArtistRepository
{
    private readonly StopifyDbContext _context;

    public UserArtistRepository(StopifyDbContext context) : base(context) =>
        _context = context;

    public async Task<IEnumerable<UserArtist>?> GetAllByArtistIdAsync(int artistId, Expression<Func<UserArtist, bool>>? expression = null) =>
        await _context.UserArtists.Where(e => e.ArtistId == artistId)
        .Include(e => e.Artist)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<UserArtist>?> GetAllByFollowedDateAsync(DateTime followedDate, Expression<Func<UserArtist, bool>>? expression = null) =>
        await _context.UserArtists.Where(e => e.FollowedDate == followedDate)
        .Include(e => e.Artist)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<UserArtist>?> GetAllByUserIdAsync(int userId, Expression<Func<UserArtist, bool>>? expression = null) =>
        await _context.UserArtists.Where(e => e.UserId == userId)
        .Include(e => e.Artist)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<UserArtist?> GetFirstByArtistIdAsync(int artistId, Expression<Func<UserArtist, bool>>? expression = null) =>
        await _context.UserArtists.Where(e => e.ArtistId == artistId)
        .Include(e => e.Artist)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<UserArtist?> GetFirstByFollowedDateAsync(DateTime followedDate, Expression<Func<UserArtist, bool>>? expression = null) =>
        await _context.UserArtists.Where(e => e.FollowedDate == followedDate)
        .Include(e => e.Artist)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<UserArtist?> GetFirstByUserIdAsync(int userId, Expression<Func<UserArtist, bool>>? expression = null) =>
        await _context.UserArtists.Where(e => e.UserId == userId)
        .Include(e => e.Artist)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();
}
