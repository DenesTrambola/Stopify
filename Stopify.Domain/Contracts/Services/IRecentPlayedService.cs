using Stopify.Domain.Contracts.Common;
using Stopify.Domain.DTOs;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Services;

public interface IRecentPlayedService : IEntityService<RecentPlayed, RecentPlayedDto>
{
    Task<RecentPlayed?> GetAsync(int userId, int songId, int position, Expression<Func<SongPlaylist, bool>>? expression = null);
    Task<RecentPlayed?> GetFirstByUserIdAsync(int userId, Expression<Func<RecentPlayed, bool>>? expression = null);
    Task<RecentPlayed?> GetFirstBySongIdAsync(int songId, Expression<Func<RecentPlayed, bool>>? expression = null);
    Task<RecentPlayed?> GetFirstByPositionAsync(int position, Expression<Func<RecentPlayed, bool>>? expression = null);

    Task<IEnumerable<RecentPlayed>?> GetAllByUserIdAsync(int userId, Expression<Func<RecentPlayed, bool>>? expression = null);
    Task<IEnumerable<RecentPlayed>?> GetAllBySongIdAsync(int songId, Expression<Func<RecentPlayed, bool>>? expression = null);
    Task<IEnumerable<RecentPlayed>?> GetAllByPositionAsync(int position, Expression<Func<RecentPlayed, bool>>? expression = null);

    Task UpdateUserAsync(RecentPlayed recentItem, string newUsername);
    Task UpdateSongAsync(RecentPlayed recentItem, string newSongTitle);
    Task UpdatePositionAsync(RecentPlayed recentItem, int newPosition);
}
