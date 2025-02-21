using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Repositories;

public interface IUserPlaylistRepository : IRepository<UserPlaylist>
{
    Task<IEnumerable<UserPlaylist>?> GetAllByPlaylistIdAsync(int playlistId, Expression<Func<UserPlaylist, bool>>? expression = null);
    Task<IEnumerable<UserPlaylist>?> GetAllBySavedDateAsync(DateTime savedDate, Expression<Func<UserPlaylist, bool>>? expression = null);
    Task<IEnumerable<UserPlaylist>?> GetAllByUserIdAsync(int userId, Expression<Func<UserPlaylist, bool>>? expression = null);
    Task<UserPlaylist?> GetFirstByPlaylistIdAsync(int playlistId, Expression<Func<UserPlaylist, bool>>? expression = null);
    Task<UserPlaylist?> GetFirstBySavedDateAsync(DateTime savedDate, Expression<Func<UserPlaylist, bool>>? expression = null);
    Task<UserPlaylist?> GetFirstByUserIdAsync(int userId, Expression<Func<UserPlaylist, bool>>? expression = null);
}
