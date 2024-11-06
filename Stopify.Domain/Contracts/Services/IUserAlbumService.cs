using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Services;

public interface IUserAlbumService : IService<UserAlbum, UserAlbum>
{
    Task<UserAlbum> GetAsync(int userId, int albumId, Expression<Func<SongPlaylist, bool>>? expression = null);
    Task<UserAlbum?> GetFirstByUserIdAsync(int userId, Expression<Func<UserAlbum, bool>>? expression = null);
    Task<UserAlbum?> GetFirstByAlbumIdAsync(int albumId, Expression<Func<UserAlbum, bool>>? expression = null);
    Task<UserAlbum?> GetFirstBySaveDateAsync(DateTime savedDate, Expression<Func<UserAlbum, bool>>? expression = null);

    Task<IEnumerable<UserAlbum>?> GetAllByUserIdAsync(int userId, Expression<Func<UserAlbum, bool>>? expression = null);
    Task<IEnumerable<UserAlbum>?> GetAllByAlbumIdAsync(int albumId, Expression<Func<UserAlbum, bool>>? expression = null);
    Task<IEnumerable<UserAlbum>?> GetAllBySavedDateAsync(DateTime savedDate, Expression<Func<UserAlbum, bool>>? expression = null);

    Task UpdateUserAsync(UserAlbum userAlbumsItem, string newUsername);
    Task UpdateAlbumAsync(UserAlbum userAlbumsItem, string newAlbumTitle);
    Task UpdateSavedDateAsync(UserAlbum userAlbumsItem, DateTime newSavedDate);

    Task CreateAsync(string username, string albumTitle);
}
