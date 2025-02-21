using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Repositories;

public interface IRecentPlayedRepository : IEntityRepository<RecentPlayed>
{
    Task<RecentPlayed?> GetFirstByUserIdAsync(int userId, Expression<Func<RecentPlayed, bool>>? expression = null);
    Task<RecentPlayed?> GetFirstBySongIdAsync(int songId, Expression<Func<RecentPlayed, bool>>? expression = null);
    Task<RecentPlayed?> GetFirstByPositionAsync(int position, Expression<Func<RecentPlayed, bool>>? expression = null);

    Task<IEnumerable<RecentPlayed>?> GetAllByUserIdAsync(int userId, Expression<Func<RecentPlayed, bool>>? expression = null);
    Task<IEnumerable<RecentPlayed>?> GetAllBySongIdAsync(int songId, Expression<Func<RecentPlayed, bool>>? expression = null);
    Task<IEnumerable<RecentPlayed>?> GetAllByPositionAsync(int position, Expression<Func<RecentPlayed, bool>>? expression = null);
}
