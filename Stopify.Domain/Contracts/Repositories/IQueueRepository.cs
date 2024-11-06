using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Repositories;

public interface IQueueRepository : IEntityRepository<SongQueue>
{
    Task<SongQueue?> GetFirstByUserIdAsync(int userId, Expression<Func<SongQueue, bool>>? expression = null);
    Task<SongQueue?> GetFirstBySongIdAsync(int songId, Expression<Func<SongQueue, bool>>? expression = null);
    Task<SongQueue?> GetFirstByPositionAsync(int position, Expression<Func<SongQueue, bool>>? expression = null);

    Task<IEnumerable<SongQueue>?> GetAllByUserIdAsync(int userId, Expression<Func<SongQueue, bool>>? expression = null);
    Task<IEnumerable<SongQueue>?> GetAllBySongIdAsync(int songId, Expression<Func<SongQueue, bool>>? expression = null);
    Task<IEnumerable<SongQueue>?> GetAllByPositionAsync(int position, Expression<Func<SongQueue, bool>>? expression = null);

}
