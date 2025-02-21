using Stopify.Domain.Contracts.Common;
using Stopify.Domain.DTOs;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Services;

public interface IUserService : IEntityService<User, UserDto>
{
    Task<User?> GetByUsernameAsync(string username, Expression<Func<User, bool>>? expression = null);
    Task<User?> GetFirstByFirstNameAsync(string firstName, Expression<Func<User, bool>>? expression = null);
    Task<User?> GetFirstByLastNameAsync(string lastName, Expression<Func<User, bool>>? expression = null);
    Task<User?> GetFirstByFullNameAsync(string firstName, string lastName, Expression<Func<User, bool>>? expression = null);
    Task<User?> GetByEmailAsync(string email, Expression<Func<User, bool>>? expression = null);
    Task<User?> GetFirstByPasswordAsync(string password, Expression<Func<User, bool>>? expression = null);
    Task<User?> GetFirstByDateJoinedAsync(DateTime dateJoined, Expression<Func<User, bool>>? expression = null);
    Task<User?> GetFirstByAvatarAsync(string avatar, Expression<Func<User, bool>>? expression = null);

    Task<IEnumerable<User>?> GetAllByFirstNameAsync(string firstName, Expression<Func<User, bool>>? expression = null);
    Task<IEnumerable<User>?> GetAllByLastNameAsync(string lastName, Expression<Func<User, bool>>? expression = null);
    Task<IEnumerable<User>?> GetAllByFullNameAsync(string firstName, string lastName, Expression<Func<User, bool>>? expression = null);
    Task<IEnumerable<User>?> GetAllByPasswordAsync(string password, Expression<Func<User, bool>>? expression = null);
    Task<IEnumerable<User>?> GetAllByDateJoinedAsync(DateTime dateJoined, Expression<Func<User, bool>>? expression = null);
    Task<IEnumerable<User>?> GetAllByAvatarAsync(string avatar, Expression<Func<User, bool>>? expression = null);

    Task UpdateUsernameAsync(string username, string newUsername);
    Task UpdateFirstNameAsync(string username, string newFirstName);
    Task UpdateLastNameAsync(string username, string lastName);
    Task UpdateFullNameAsync(string username, string newFirstName, string newLastName);
    Task UpdateEmailAsync(string username, string newEmail);
    Task UpdatePasswordAsync(string username, string newPassword);
    Task UpdateAvatarAsync(string username, string newAvatar);

    Task AddQueueAsync(string username, string songTitle, int? position = null);
    Task AddRecentPlayedAsync(string username, string songTitle, int? position = null);
    Task AddAlbumAsync(string username, string albumTitle);
    Task AddArtistAsync(string username, string artistName);
    Task AddPlaybackHistoryAsync(string username, string songTitle, int? position = null);
    Task AddPlaylistAsync(string username, string playlistTitle, int? position = null);
    Task AddFollowerAsync(string username, string followerUsername);
    Task AddFollowingAsync(string username, string followingUsername);
}
