using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Services;

public interface IUserPlaylistService : IService<UserPlaylist, UserPlaylist>
{
    Task<UserPlaylist> GetAsync(int userId, int playlistId, Expression<Func<SongPlaylist, bool>>? expression = null);
    Task<UserPlaylist?> GetFirstByUserIdAsync(int userId, Expression<Func<UserPlaylist, bool>>? expression = null);
    Task<UserPlaylist?> GetFirstByPlaylistIdAsync(int playlistId, Expression<Func<UserPlaylist, bool>>? expression = null);
    Task<UserPlaylist?> GetFirstBySavedDateAsync(DateTime savedDate, Expression<Func<UserPlaylist, bool>>? expression = null);

    Task<IEnumerable<UserPlaylist>?> GetAllByUserIdAsync(int userId, Expression<Func<UserPlaylist, bool>>? expression = null);
    Task<IEnumerable<UserPlaylist>?> GetAllByPlaylistIdAsync(int playlistId, Expression<Func<UserPlaylist, bool>>? expression = null);
    Task<IEnumerable<UserPlaylist>?> GetAllBySavedDateAsync(DateTime savedDate, Expression<Func<UserPlaylist, bool>>? expression = null);

    Task UpdateUserAsync(UserPlaylist userPlaylistsItem, string newUsername);
    Task UpdatePlaylistAsync(UserPlaylist userPlaylistsItem, string newPlaylistTitle);
    Task UpdateSavedDateAsync(UserPlaylist userPlaylistsItem, DateTime newSavedDate);

    Task CreateAsync(string username, string playlistTitle);
}
