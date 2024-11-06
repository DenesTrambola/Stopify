using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Repositories;

public interface IUserArtistRepository : IRepository<UserArtist>
{
    Task<UserArtist?> GetFirstByUserIdAsync(int userId, Expression<Func<UserArtist, bool>>? expression = null);
    Task<UserArtist?> GetFirstByArtistIdAsync(int artistId, Expression<Func<UserArtist, bool>>? expression = null);
    Task<UserArtist?> GetFirstByFollowedDateAsync(DateTime followedDate, Expression<Func<UserArtist, bool>>? expression = null);

    Task<IEnumerable<UserArtist>?> GetAllByUserIdAsync(int userId, Expression<Func<UserArtist, bool>>? expression = null);
    Task<IEnumerable<UserArtist>?> GetAllByArtistIdAsync(int artistId, Expression<Func<UserArtist, bool>>? expression = null);
    Task<IEnumerable<UserArtist>?> GetAllByFollowedDateAsync(DateTime followedDate, Expression<Func<UserArtist, bool>>? expression = null);
}
