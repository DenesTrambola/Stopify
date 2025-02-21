using Microsoft.EntityFrameworkCore;
using Stopify.Domain.Contracts.Repositories;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Infrastructure.Persistence.Repositories;

public class QueueRepository : GenericEntityRepository<SongQueue>, IQueueRepository
{
    private readonly StopifyDbContext _context;

    public QueueRepository(StopifyDbContext context) : base(context) =>
        _context = context;

    public async Task<IEnumerable<SongQueue>?> GetAllByPositionAsync(int position, Expression<Func<SongQueue, bool>>? expression = null) =>
        await _context.Queues.Where(e => e.Position == position)
        .Include(e => e.Song)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<SongQueue>?> GetAllBySongIdAsync(int songId, Expression<Func<SongQueue, bool>>? expression = null) =>
        await _context.Queues.Where(e => e.SongId == songId)
        .Include(e => e.Song)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<SongQueue>?> GetAllByUserIdAsync(int userId, Expression<Func<SongQueue, bool>>? expression = null) =>
        await _context.Queues.Where(e => e.UserId == userId)
        .Include(e => e.Song)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<SongQueue?> GetFirstByPositionAsync(int position, Expression<Func<SongQueue, bool>>? expression = null) =>
        await _context.Queues.Where(e => e.Position == position)
        .Include(e => e.Song)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<SongQueue?> GetFirstBySongIdAsync(int songId, Expression<Func<SongQueue, bool>>? expression = null) =>
        await _context.Queues.Where(e => e.SongId == songId)
        .Include(e => e.Song)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<SongQueue?> GetFirstByUserIdAsync(int userId, Expression<Func<SongQueue, bool>>? expression = null) =>
        await _context.Queues.Where(e => e.UserId == userId)
        .Include(e => e.Song)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();
}
