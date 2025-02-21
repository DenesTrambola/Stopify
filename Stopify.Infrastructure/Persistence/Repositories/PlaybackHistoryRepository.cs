using Microsoft.EntityFrameworkCore;
using Stopify.Domain.Contracts.Repositories;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Infrastructure.Persistence.Repositories;

public class PlaybackHistoryRepository : GenericEntityRepository<PlaybackHistory>, IPlaybackHistoryRepository
{
    private readonly StopifyDbContext _context;

    public PlaybackHistoryRepository(StopifyDbContext context) : base(context) =>
        _context = context;

    public async Task<IEnumerable<PlaybackHistory>?> GetAllByPlaybackDateTimeAsync(DateTime playbackDateTime, Expression<Func<PlaybackHistory, bool>>? expression = null) =>
        await _context.PlaybackHistories.Where(e => e.PlaybackDateTime == playbackDateTime)
        .Include(e => e.Song)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<PlaybackHistory>?> GetAllByPositionAsync(int position, Expression<Func<PlaybackHistory, bool>>? expression = null) =>
        await _context.PlaybackHistories.Where(e => e.Position == position)
        .Include(e => e.Song)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<PlaybackHistory>?> GetAllBySongIdAsync(int songId, Expression<Func<PlaybackHistory, bool>>? expression = null) =>
        await _context.PlaybackHistories.Where(e => e.SongId == songId)
        .Include(e => e.Song)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<PlaybackHistory>?> GetAllByUserIdAsync(int userId, Expression<Func<PlaybackHistory, bool>>? expression = null) =>
        await _context.PlaybackHistories.Where(e => e.UserId == userId)
        .Include(e => e.Song)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<PlaybackHistory?> GetFirstByPlaybackDateTimeAsync(DateTime playbackDateTime, Expression<Func<PlaybackHistory, bool>>? expression = null) =>
        await _context.PlaybackHistories.Where(e => e.PlaybackDateTime == playbackDateTime)
        .Include(e => e.Song)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<PlaybackHistory?> GetFirstByPositionAsync(int position, Expression<Func<PlaybackHistory, bool>>? expression = null) =>
        await _context.PlaybackHistories.Where(e => e.Position == position)
        .Include(e => e.Song)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<PlaybackHistory?> GetFirstBySongIdAsync(int songId, Expression<Func<PlaybackHistory, bool>>? expression = null) =>
        await _context.PlaybackHistories.Where(e => e.SongId == songId)
        .Include(e => e.Song)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<PlaybackHistory?> GetFirstByUserIdAsync(int userId, Expression<Func<PlaybackHistory, bool>>? expression = null) =>
        await _context.PlaybackHistories.Where(e => e.UserId == userId)
        .Include(e => e.Song)
        .Include(e => e.User)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();
}
