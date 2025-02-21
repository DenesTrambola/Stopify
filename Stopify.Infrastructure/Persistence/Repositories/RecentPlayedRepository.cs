using Microsoft.EntityFrameworkCore;
using Stopify.Domain.Contracts.Repositories;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Infrastructure.Persistence.Repositories;

public class RecentPlayedRepository : GenericEntityRepository<RecentPlayed>, IRecentPlayedRepository
{
    private readonly StopifyDbContext _context;

    public RecentPlayedRepository(StopifyDbContext context) : base(context) =>
        _context = context;

    public async Task<IEnumerable<RecentPlayed>?> GetAllByPositionAsync(int position, Expression<Func<RecentPlayed, bool>>? expression = null) =>
        await _context.RecentPlays.Where(e => e.Position == position)
        .Include(e => e.Song)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<RecentPlayed>?> GetAllBySongIdAsync(int songId, Expression<Func<RecentPlayed, bool>>? expression = null) =>
        await _context.RecentPlays.Where(e => e.SongId == songId)
        .Include(e => e.Song)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<RecentPlayed>?> GetAllByUserIdAsync(int userId, Expression<Func<RecentPlayed, bool>>? expression = null) =>
        await _context.RecentPlays.Where(e => e.UserId == userId)
        .Include(e => e.Song)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<RecentPlayed?> GetFirstByPositionAsync(int position, Expression<Func<RecentPlayed, bool>>? expression = null) =>
        await _context.RecentPlays.Where(e => e.Position == position)
        .Include(e => e.Song)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<RecentPlayed?> GetFirstBySongIdAsync(int songId, Expression<Func<RecentPlayed, bool>>? expression = null) =>
        await _context.RecentPlays.Where(e => e.SongId == songId)
        .Include(e => e.Song)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<RecentPlayed?> GetFirstByUserIdAsync(int userId, Expression<Func<RecentPlayed, bool>>? expression = null) =>
        await _context.RecentPlays.Where(e => e.UserId == userId)
        .Include(e => e.Song)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();
}
