using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Services;

public interface IUserArtistService : IService<UserArtist, UserArtist>
{
    Task<UserArtist> GetAsync(int userId, int artistId, Expression<Func<SongPlaylist, bool>>? expression = null);
    Task<UserArtist?> GetFirstByUserIdAsync(int userId, Expression<Func<UserArtist, bool>>? expression = null);
    Task<UserArtist?> GetFirstByArtistIdAsync(int artistId, Expression<Func<UserArtist, bool>>? expression = null);
    Task<UserArtist?> GetFirstByFollowedDateAsync(DateTime followedDate, Expression<Func<UserArtist, bool>>? expression = null);

    Task<IEnumerable<UserArtist>?> GetAllByUserIdAsync(int userId, Expression<Func<UserArtist, bool>>? expression = null);
    Task<IEnumerable<UserArtist>?> GetAllByArtistIdAsync(int artistId, Expression<Func<UserArtist, bool>>? expression = null);
    Task<IEnumerable<UserArtist>?> GetAllByFollowedDateAsync(DateTime followedDate, Expression<Func<UserArtist, bool>>? expression = null);

    Task UpdateUserAsync(UserArtist userArtistsItem, string newUsername);
    Task UpdateArtistAsync(UserArtist userArtistsItem, string newArtistName);
    Task UpdateFollowedDateAsync(UserArtist userArtistsItem, DateTime newFollowedDate);

    Task CreateAsync(string username, string artistName);
}
