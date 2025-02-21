using Microsoft.EntityFrameworkCore;
using Stopify.Domain.Contracts.Repositories;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Infrastructure.Persistence.Repositories;

public class UserFollowerRepository : GenericRepository<UserFollower>, IUserFollowerRepository
{
    private readonly StopifyDbContext _context;

    public UserFollowerRepository(StopifyDbContext context) : base(context) =>
        _context = context;

    public async Task<IEnumerable<UserFollower>?> GetAllByFollowerIdAsync(int followerId, Expression<Func<UserFollower, bool>>? expression = null) =>
        await _context.UserFollowers.Where(e => e.FollowerId == followerId)
        .Include(e => e.User)
        .Include(e => e.Follower)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<UserFollower>?> GetAllByFollowedDateAsync(DateTime followedDate, Expression<Func<UserFollower, bool>>? expression = null) =>
        await _context.UserFollowers.Where(e => e.FollowedDate == followedDate)
        .Include(e => e.User)
        .Include(e => e.Follower)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<UserFollower>?> GetAllByUserIdAsync(int userId, Expression<Func<UserFollower, bool>>? expression = null) =>
        await _context.UserFollowers.Where(e => e.UserId == userId)
        .Include(e => e.User)
        .Include(e => e.Follower)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<UserFollower?> GetFirstByFollowerIdAsync(int followerId, Expression<Func<UserFollower, bool>>? expression = null) =>
        await _context.UserFollowers.Where(e => e.FollowerId == followerId)
        .Include(e => e.User)
        .Include(e => e.Follower)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<UserFollower?> GetFirstByFollowedDateAsync(DateTime followedDate, Expression<Func<UserFollower, bool>>? expression = null) =>
        await _context.UserFollowers.Where(e => e.FollowedDate == followedDate)
        .Include(e => e.User)
        .Include(e => e.Follower)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<UserFollower?> GetFirstByUserIdAsync(int userId, Expression<Func<UserFollower, bool>>? expression = null) =>
        await _context.UserFollowers.Where(e => e.UserId == userId)
        .Include(e => e.User)
        .Include(e => e.Follower)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();
}
