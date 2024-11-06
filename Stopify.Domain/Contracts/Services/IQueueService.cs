using Stopify.Domain.Contracts.Common;
using Stopify.Domain.DTOs;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Services;

public interface IQueueService : IEntityService<SongQueue, QueueDto>
{
    Task<SongQueue?> GetAsync(int userId, int songId, int position, Expression<Func<PlaybackHistory, bool>>? expression = null);
    Task<SongQueue?> GetFirstByUserIdAsync(int userId, Expression<Func<SongQueue, bool>>? expression = null);
    Task<SongQueue?> GetFirstBySongIdAsync(int songId, Expression<Func<SongQueue, bool>>? expression = null);
    Task<SongQueue?> GetFirstByPositionAsync(int position, Expression<Func<SongQueue, bool>>? expression = null);

    Task<IEnumerable<SongQueue>?> GetAllByUserIdAsync(int userId, Expression<Func<SongQueue, bool>>? expression = null);
    Task<IEnumerable<SongQueue>?> GetAllBySongIdAsync(int songId, Expression<Func<SongQueue, bool>>? expression = null);
    Task<IEnumerable<SongQueue>?> GetAllByPositionAsync(int position, Expression<Func<SongQueue, bool>>? expression = null);

    Task UpdateUserAsync(SongQueue queueItem, string newUsername);
    Task UpdateSongAsync(SongQueue queueItem, string newSongTitle);
    Task UpdatePositionAsync(SongQueue queueItem, int newPosition);
}
