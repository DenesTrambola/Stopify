using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Repositories;

public interface IUserAlbumRepository : IRepository<UserAlbum>
{
    Task<UserAlbum?> GetFirstByUserIdAsync(int userId, Expression<Func<UserAlbum, bool>>? expression = null);
    Task<UserAlbum?> GetFirstByAlbumIdAsync(int albumId, Expression<Func<UserAlbum, bool>>? expression = null);
    Task<UserAlbum?> GetFirstBySaveDateAsync(DateTime savedDate, Expression<Func<UserAlbum, bool>>? expression = null);

    Task<IEnumerable<UserAlbum>?> GetAllByUserIdAsync(int userId, Expression<Func<UserAlbum, bool>>? expression = null);
    Task<IEnumerable<UserAlbum>?> GetAllByAlbumIdAsync(int albumId, Expression<Func<UserAlbum, bool>>? expression = null);
    Task<IEnumerable<UserAlbum>?> GetAllBySavedDateAsync(DateTime savedDate, Expression<Func<UserAlbum, bool>>? expression = null);
}
