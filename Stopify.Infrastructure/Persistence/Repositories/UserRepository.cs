using Microsoft.EntityFrameworkCore;
using Stopify.Domain.Contracts.Repositories;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Infrastructure.Persistence.Repositories;

public class UserRepository : GenericEntityRepository<User>, IUserRepository
{
    private readonly StopifyDbContext _context;

    public UserRepository(StopifyDbContext context) : base(context) =>
        _context = context;

    public async Task<IEnumerable<User>?> GetAllByDateJoinedAsync(DateTime dateJoined, Expression<Func<User, bool>>? expression = null) =>
        await _context.Users.Where(e => e.DateJoined == dateJoined)
        .Include(e => e.Queues)
        .Include(e => e.RecentPlays)
        .Include(e => e.UserAlbums)
        .Include(e => e.UserArtists)
        .Include(e => e.PlaybackHistories)
        .Include(e => e.UserPlaylists)
        .Include(e => e.Followers)
        .Include(e => e.Followings)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<User>?> GetAllByFirstNameAsync(string firstName, Expression<Func<User, bool>>? expression = null) =>
        await _context.Users.Where(e => e.FirstName == firstName)
        .Include(e => e.Queues)
        .Include(e => e.RecentPlays)
        .Include(e => e.UserAlbums)
        .Include(e => e.UserArtists)
        .Include(e => e.PlaybackHistories)
        .Include(e => e.UserPlaylists)
        .Include(e => e.Followers)
        .Include(e => e.Followings)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<User>?> GetAllByFullNameAsync(string firstName, string lastName, Expression<Func<User, bool>>? expression = null) =>
        await _context.Users.Where(e => e.FirstName == firstName && e.LastName == lastName)
        .Include(e => e.Queues)
        .Include(e => e.RecentPlays)
        .Include(e => e.UserAlbums)
        .Include(e => e.UserArtists)
        .Include(e => e.PlaybackHistories)
        .Include(e => e.UserPlaylists)
        .Include(e => e.Followers)
        .Include(e => e.Followings)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<User>?> GetAllByLastNameAsync(string lastName, Expression<Func<User, bool>>? expression = null) =>
        await _context.Users.Where(e => e.LastName == lastName)
        .Include(e => e.Queues)
        .Include(e => e.RecentPlays)
        .Include(e => e.UserAlbums)
        .Include(e => e.UserArtists)
        .Include(e => e.PlaybackHistories)
        .Include(e => e.UserPlaylists)
        .Include(e => e.Followers)
        .Include(e => e.Followings)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<User>?> GetAllByPasswordHashAsync(string passwordHash, Expression<Func<User, bool>>? expression = null) =>
        await _context.Users.Where(e => e.PasswordHash == passwordHash)
        .Include(e => e.Queues)
        .Include(e => e.RecentPlays)
        .Include(e => e.UserAlbums)
        .Include(e => e.UserArtists)
        .Include(e => e.PlaybackHistories)
        .Include(e => e.UserPlaylists)
        .Include(e => e.Followers)
        .Include(e => e.Followings)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<User>?> GetAllByAvatarAsync(string avatar, Expression<Func<User, bool>>? expression = null) =>
        await _context.Users.Where(e => e.Avatar == avatar)
        .Include(e => e.Queues)
        .Include(e => e.RecentPlays)
        .Include(e => e.UserAlbums)
        .Include(e => e.UserArtists)
        .Include(e => e.PlaybackHistories)
        .Include(e => e.UserPlaylists)
        .Include(e => e.Followers)
        .Include(e => e.Followings)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<User?> GetByEmailAsync(string email, Expression<Func<User, bool>>? expression = null) =>
        await _context.Users.Where(e => e.Email == email)
        .Include(e => e.Queues)
        .Include(e => e.RecentPlays)
        .Include(e => e.UserAlbums)
        .Include(e => e.UserArtists)
        .Include(e => e.PlaybackHistories)
        .Include(e => e.UserPlaylists)
        .Include(e => e.Followers)
        .Include(e => e.Followings)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<User?> GetFirstByDateJoinedAsync(DateTime dateJoined, Expression<Func<User, bool>>? expression = null) =>
        await _context.Users.Where(e => e.DateJoined == dateJoined)
        .Include(e => e.Queues)
        .Include(e => e.RecentPlays)
        .Include(e => e.UserAlbums)
        .Include(e => e.UserArtists)
        .Include(e => e.PlaybackHistories)
        .Include(e => e.UserPlaylists)
        .Include(e => e.Followers)
        .Include(e => e.Followings)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<User?> GetFirstByFirstNameAsync(string firstName, Expression<Func<User, bool>>? expression = null) =>
        await _context.Users.Where(e => e.FirstName == firstName)
        .Include(e => e.Queues)
        .Include(e => e.RecentPlays)
        .Include(e => e.UserAlbums)
        .Include(e => e.UserArtists)
        .Include(e => e.PlaybackHistories)
        .Include(e => e.UserPlaylists)
        .Include(e => e.Followers)
        .Include(e => e.Followings)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<User?> GetFirstByFullNameAsync(string firstName, string lastName, Expression<Func<User, bool>>? expression = null) =>
        await _context.Users.Where(e => e.FirstName == firstName && e.LastName == lastName)
        .Include(e => e.Queues)
        .Include(e => e.RecentPlays)
        .Include(e => e.UserAlbums)
        .Include(e => e.UserArtists)
        .Include(e => e.PlaybackHistories)
        .Include(e => e.UserPlaylists)
        .Include(e => e.Followers)
        .Include(e => e.Followings)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<User?> GetFirstByLastNameAsync(string lastName, Expression<Func<User, bool>>? expression = null) =>
        await _context.Users.Where(e => e.LastName == lastName)
        .Include(e => e.Queues)
        .Include(e => e.RecentPlays)
        .Include(e => e.UserAlbums)
        .Include(e => e.UserArtists)
        .Include(e => e.PlaybackHistories)
        .Include(e => e.UserPlaylists)
        .Include(e => e.Followers)
        .Include(e => e.Followings)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<User?> GetFirstByPasswordHashAsync(string passwordHash, Expression<Func<User, bool>>? expression = null) =>
        await _context.Users.Where(e => e.PasswordHash == passwordHash)
        .Include(e => e.Queues)
        .Include(e => e.RecentPlays)
        .Include(e => e.UserAlbums)
        .Include(e => e.UserArtists)
        .Include(e => e.PlaybackHistories)
        .Include(e => e.UserPlaylists)
        .Include(e => e.Followers)
        .Include(e => e.Followings)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<User?> GetFirstByAvatarAsync(string avatar, Expression<Func<User, bool>>? expression = null) =>
        await _context.Users.Where(e => e.Avatar == avatar)
        .Include(e => e.Queues)
        .Include(e => e.RecentPlays)
        .Include(e => e.UserAlbums)
        .Include(e => e.UserArtists)
        .Include(e => e.PlaybackHistories)
        .Include(e => e.UserPlaylists)
        .Include(e => e.Followers)
        .Include(e => e.Followings)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<User?> GetByUsernameAsync(string username, Expression<Func<User, bool>>? expression = null) =>
        await _context.Users.Where(e => e.Username == username)
        .Include(e => e.Queues)
        .Include(e => e.RecentPlays)
        .Include(e => e.UserAlbums)
        .Include(e => e.UserArtists)
        .Include(e => e.PlaybackHistories)
        .Include(e => e.UserPlaylists)
        .Include(e => e.Followers)
        .Include(e => e.Followings)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();
}
