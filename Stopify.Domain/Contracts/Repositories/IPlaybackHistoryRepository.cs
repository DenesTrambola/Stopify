using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Repositories;

public interface IPlaybackHistoryRepository : IEntityRepository<PlaybackHistory>
{
    Task<PlaybackHistory?> GetFirstByUserIdAsync(int userId, Expression<Func<PlaybackHistory, bool>>? expression = null);
    Task<PlaybackHistory?> GetFirstBySongIdAsync(int songId, Expression<Func<PlaybackHistory, bool>>? expression = null);
    Task<PlaybackHistory?> GetFirstByPlaybackDateTimeAsync(DateTime playbackDateTime, Expression<Func<PlaybackHistory, bool>>? expression = null);
    Task<PlaybackHistory?> GetFirstByPositionAsync(int position, Expression<Func<PlaybackHistory, bool>>? expression = null);

    Task<IEnumerable<PlaybackHistory>?> GetAllByUserIdAsync(int userId, Expression<Func<PlaybackHistory, bool>>? expression = null);
    Task<IEnumerable<PlaybackHistory>?> GetAllBySongIdAsync(int songId, Expression<Func<PlaybackHistory, bool>>? expression = null);
    Task<IEnumerable<PlaybackHistory>?> GetAllByPlaybackDateTimeAsync(DateTime playbackDateTime, Expression<Func<PlaybackHistory, bool>>? expression = null);
    Task<IEnumerable<PlaybackHistory>?> GetAllByPositionAsync(int position, Expression<Func<PlaybackHistory, bool>>? expression = null);
}
