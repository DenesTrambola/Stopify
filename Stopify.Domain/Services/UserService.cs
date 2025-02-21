using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Contracts.Services;
using Stopify.Domain.DTOs;
using Stopify.Domain.Entities;
using Stopify.Domain.Other;
using Stopify.Exceptions.ValidationExceptions;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Stopify.Domain.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unit;
    private readonly IEmailService _emailService;
    private readonly IQueueService _queueService;
    private readonly IRecentPlayedService _recentPlayedService;
    private readonly IUserAlbumService _userAlbumService;
    private readonly IUserArtistService _userArtistService;
    private readonly IPlaybackHistoryService _playbackHistoryService;
    private readonly IUserPlaylistService _userPlaylistService;
    private readonly IUserFollowerService _userFollowerService;

    public UserService(IUnitOfWork unitOfWork,
                       IEmailService emailService,
                       IQueueService queueService,
                       IRecentPlayedService recentPlayedService,
                       IUserAlbumService userAlbumService,
                       IUserArtistService userArtistService,
                       IPlaybackHistoryService playbackHistoryService,
                       IUserPlaylistService userPlaylistService,
                       IUserFollowerService userFollowerService)
    {
        _unit = unitOfWork;
        _emailService = emailService;
        _queueService = queueService;
        _recentPlayedService = recentPlayedService;
        _userAlbumService = userAlbumService;
        _userArtistService = userArtistService;
        _playbackHistoryService = playbackHistoryService;
        _userPlaylistService = userPlaylistService;
        _userFollowerService = userFollowerService;
    }

    public async Task CreateAsync(User entity)
    {
        var userByUsername = await _unit.Users.GetByUsernameAsync(entity.Username);
        if (userByUsername != null)
            throw new EntityAlreadyExistsException(nameof(User));

        var userByEmail = await _unit.Users.GetByEmailAsync(entity.Email);
        if (userByEmail != null)
            throw new EntityAlreadyExistsException(nameof(User));

        _emailService.ProcessVerificationCode(entity.Email);

        await _unit.Users.AddAsync(entity);
        await _unit.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>?> GetAllAsync() =>
        await _unit.Users.GetAllAsync();

    public async Task<IEnumerable<User>?> GetAllAsync(Expression<Func<User, bool>>? expression) =>
        await _unit.Users.GetAllAsync(expression);

    public async Task<IEnumerable<User>?> GetAllByDateJoinedAsync(DateTime dateJoined, Expression<Func<User, bool>>? expression = null) =>
        await _unit.Users.GetAllByDateJoinedAsync(dateJoined, expression);

    public async Task<IEnumerable<User>?> GetAllByFirstNameAsync(string firstName, Expression<Func<User, bool>>? expression = null) =>
        await _unit.Users.GetAllByFirstNameAsync(firstName, expression);

    public async Task<IEnumerable<User>?> GetAllByFullNameAsync(string firstName, string lastName, Expression<Func<User, bool>>? expression = null) =>
        await _unit.Users.GetAllByFullNameAsync(firstName, lastName, expression);

    public async Task<IEnumerable<User>?> GetAllByLastNameAsync(string lastName, Expression<Func<User, bool>>? expression = null) =>
        await _unit.Users.GetAllByLastNameAsync(lastName, expression);

    public async Task<IEnumerable<User>?> GetAllByPasswordAsync(string password, Expression<Func<User, bool>>? expression = null) =>
        await _unit.Users.GetAllByPasswordHashAsync(BCrypt.Net.BCrypt.HashPassword(password), expression);

    public async Task<IEnumerable<User>?> GetAllByAvatarAsync(string avatar, Expression<Func<User, bool>>? expression = null) =>
        await _unit.Users.GetAllByAvatarAsync(avatar, expression);

    public async Task<User?> GetByIdAsync(int id, Expression<Func<User, bool>>? expression = null) =>
        await _unit.Users.GetByIdAsync(id, expression);

    public async Task<User?> GetFirstByDateJoinedAsync(DateTime dateJoined, Expression<Func<User, bool>>? expression = null) =>
        await _unit.Users.GetFirstByDateJoinedAsync(dateJoined, expression);

    public async Task<User?> GetByEmailAsync(string email, Expression<Func<User, bool>>? expression = null) =>
        await _unit.Users.GetByEmailAsync(email, expression);

    public async Task<User?> GetFirstByFirstNameAsync(string firstName, Expression<Func<User, bool>>? expression = null) =>
        await _unit.Users.GetFirstByFirstNameAsync(firstName, expression);

    public async Task<User?> GetFirstByFullNameAsync(string firstName, string lastName, Expression<Func<User, bool>>? expression = null) =>
        await _unit.Users.GetFirstByFullNameAsync(firstName, lastName, expression);

    public async Task<User?> GetFirstByLastNameAsync(string lastName, Expression<Func<User, bool>>? expression = null) =>
        await _unit.Users.GetFirstByLastNameAsync(lastName, expression);

    public async Task<User?> GetFirstByPasswordAsync(string password, Expression<Func<User, bool>>? expression = null) =>
        await _unit.Users.GetFirstByPasswordHashAsync(BCrypt.Net.BCrypt.HashPassword(password), expression);

    public async Task<User?> GetFirstByAvatarAsync(string avatar, Expression<Func<User, bool>>? expression = null) =>
        await _unit.Users.GetFirstByAvatarAsync(avatar, expression);

    public async Task<User?> GetByUsernameAsync(string username, Expression<Func<User, bool>>? expression = null) =>
        await _unit.Users.GetByUsernameAsync(username, expression);

    public async Task RemoveAsync(UserDto dto)
    {
        var user = await _unit.Users.GetByUsernameAsync(dto.Username);
        if (user == null)
            throw new EntityNotFoundException(nameof(User));

        _unit.Users.Remove(user);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateAvatarAsync(string username, [StringLength(2048, ErrorMessage = "Maximum length is 2048!")] string newAvatar)
    {
        var user = await GetByUsernameAsync(username);
        if (user == null)
            throw new EntityNotFoundException(nameof(User));

        newAvatar = UrlValidation.CheckFormat(newAvatar) ? newAvatar : $"{User.MainAvatarPath}{newAvatar}.jpg" ?? $"{User.MainAvatarPath}user-avatar-default.jpg";
        if (!UrlValidation.CheckFormat(user.Avatar))
            throw new InvalidUrlException();

        if (user.Avatar == newAvatar)
            throw new SamePropertyNameException(nameof(User) + " " + nameof(User.Avatar));
        user.Avatar = newAvatar;

        _unit.Users.Update(user, e => e.Avatar);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateEmailAsync(string username, [StringLength(254, ErrorMessage = "Maximum length is 254!")] string newEmail)
    {
        if (EmailValidation.CheckFormat(newEmail))
            throw new InvalidEmailException();

        var user = await GetByUsernameAsync(username);
        if (user == null)
            throw new EntityNotFoundException(nameof(User));

        if (!EmailValidation.CheckFormat(newEmail))
            throw new InvalidEmailException();

        if (user.Email == newEmail)
            throw new SamePropertyNameException(nameof(User) + " " + nameof(User.Email));

        var userByEmail = await _unit.Users.GetByEmailAsync(newEmail);
        if (userByEmail == null)
            throw new EntityNotFoundException(nameof(User));

        _emailService.ProcessVerificationCode(user.Email);

        user.Email = newEmail;
        _unit.Users.Update(user, e => e.Email);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateFirstNameAsync(string username, [StringLength(50, ErrorMessage = "Maximum length is 50!")] string newFirstName)
    {
        var user = await GetByUsernameAsync(username);
        if (user == null)
            throw new EntityNotFoundException(nameof(User));

        if (user.FirstName == newFirstName)
            throw new SamePropertyNameException(nameof(User) + " " + nameof(User.FirstName));

        _emailService.ProcessVerificationCode(user.Email);

        user.FirstName = newFirstName;
        _unit.Users.Update(user, e => e.FirstName);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateLastNameAsync(string username, [StringLength(50, ErrorMessage = "Maximum length is 50!")] string newLastName)
    {
        var user = await GetByUsernameAsync(username);
        if (user == null)
            throw new EntityNotFoundException(nameof(User));

        if (user.LastName == newLastName)
            throw new SamePropertyNameException(nameof(User) + " " + nameof(User.LastName));

        _emailService.ProcessVerificationCode(user.Email);

        user.LastName = newLastName;
        _unit.Users.Update(user, e => e.LastName);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateFullNameAsync(string username,
        [StringLength(50, ErrorMessage = "Maximum length is 50!")] string newFirstName,
        [StringLength(50, ErrorMessage = "Maximum length is 50!")] string newLastName)
    {
        var user = await GetByUsernameAsync(username);
        if (user == null)
            throw new EntityNotFoundException(nameof(User));

        if (user.FirstName == newFirstName && user.LastName == newLastName)
            throw new SamePropertyNameException(nameof(User) + " FullName");

        _emailService.ProcessVerificationCode(user.Email);

        user.FirstName = newFirstName;
        user.LastName = newLastName;
        _unit.Users.Update(user, e => e.FirstName, e => e.LastName);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdatePasswordAsync(string username, [StringLength(60, ErrorMessage = "Maximum length is 60!")] string newPassword)
    {
        var user = await GetByUsernameAsync(username);
        if (user == null)
            throw new EntityNotFoundException(nameof(User));

        if (!BCrypt.Net.BCrypt.Verify(newPassword, user.PasswordHash))
            throw new SamePropertyNameException(nameof(User) + " " + nameof(User.PasswordHash));

        _emailService.ProcessVerificationCode(user.Email);

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        _unit.Users.Update(user, e => e.PasswordHash);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateUsernameAsync(string username, [StringLength(50, ErrorMessage = "Maximum length is 50!")] string newUsername)
    {
        var user = await GetByUsernameAsync(username);
        if (user == null)
            throw new EntityNotFoundException(nameof(User));

        if (user.Username == newUsername)
            throw new SamePropertyNameException(nameof(User) + " " + nameof(User.Username));

        var userByNewUsername = await _unit.Users.GetByUsernameAsync(username);
        if (userByNewUsername != null)
            throw new EntityAlreadyExistsException(nameof(User));

        _emailService.ProcessVerificationCode(user.Email);

        user.Username = newUsername;
        _unit.Users.Update(user, e => e.Username);
        await _unit.SaveChangesAsync();
    }

    public async Task AddQueueAsync(string username, string songTitle, int? position = null)
    {
        var song = await _unit.Songs.GetByTitleAsync(songTitle);
        if (song is null)
            throw new EntityNotFoundException(nameof(Song));

        var user = await _unit.Users.GetByUsernameAsync(username);
        if (user is null)
            throw new EntityNotFoundException(nameof(User));

        var queueItem = new SongQueue(user.Id, song.Id, (position is null ? user.PlaybackHistories.Count + 1 : (int)position));
        await _queueService.CreateAsync(queueItem);

        song.Queues.Add(queueItem);
        user.Queues.Add(queueItem);
    }

    public async Task AddRecentPlayedAsync(string username, string songTitle, int? position = null)
    {
        var song = await _unit.Songs.GetByTitleAsync(songTitle);
        if (song is null)
            throw new EntityNotFoundException(nameof(Song));

        var user = await _unit.Users.GetByUsernameAsync(username);
        if (user is null)
            throw new EntityNotFoundException(nameof(User));

        var recentItem = new RecentPlayed(user.Id, song.Id, (position is null ? user.PlaybackHistories.Count + 1 : (int)position));
        await _recentPlayedService.CreateAsync(recentItem);

        song.RecentPlays.Add(recentItem);
        user.RecentPlays.Add(recentItem);
    }

    public async Task AddAlbumAsync(string username, string albumTitle) =>
        await _userAlbumService.CreateAsync(username, albumTitle);

    public async Task AddArtistAsync(string username, string artistName) =>
        await _userArtistService.CreateAsync(username, artistName);

    public async Task AddPlaybackHistoryAsync(string username, string songTitle, int? position = null)
    {
        var song = await _unit.Songs.GetByTitleAsync(songTitle);
        if (song is null)
            throw new EntityNotFoundException(nameof(Song));

        var user = await _unit.Users.GetByUsernameAsync(username);
        if (user is null)
            throw new EntityNotFoundException(nameof(User));

        var historyItem = new PlaybackHistory(user.Id, song.Id, (position is null ? user.PlaybackHistories.Count + 1 : (int)position));
        await _playbackHistoryService.CreateAsync(historyItem);

        song.PlaybackHistories.Add(historyItem);
        user.PlaybackHistories.Add(historyItem);
    }

    public async Task AddPlaylistAsync(string username, string playlistTitle, int? position = null) =>
        await _userPlaylistService.CreateAsync(username, playlistTitle);

    public async Task AddFollowerAsync(string username, string followerUsername)
    {
        var user = await _unit.Users.GetByUsernameAsync(username);
        if (user is null)
            throw new EntityNotFoundException(nameof(User));

        var follower = await _unit.Users.GetByUsernameAsync(followerUsername);
        if (follower is null)
            throw new EntityNotFoundException(nameof(User));

        var userFollowerItem = new UserFollower(user.Id, follower.Id);
        await _userFollowerService.CreateAsync(userFollowerItem);

        if (user.Followers.Any(f => f.UserId == user.Id && f.FollowerId == follower.Id))
            throw new EntityAlreadyExistsException(nameof(UserFollower));

        user.Followers.Add(userFollowerItem);
        follower.Followings.Add(userFollowerItem);

        await _unit.SaveChangesAsync();
    }

    public async Task AddFollowingAsync(string username, string followingUsername)
    {
        var user = await _unit.Users.GetByUsernameAsync(username);
        if (user is null)
            throw new EntityNotFoundException(nameof(User));

        var followingUser = await _unit.Users.GetByUsernameAsync(followingUsername);
        if (followingUser is null)
            throw new EntityNotFoundException(nameof(User));

        var userFollowerItem = new UserFollower(followingUser.Id, user.Id);
        await _userFollowerService.CreateAsync(userFollowerItem);

        if (user.Followings.Any(f => f.FollowerId == user.Id && f.UserId == followingUser.Id))
            throw new EntityAlreadyExistsException(nameof(UserFollower));

        user.Followings.Add(userFollowerItem);
        followingUser.Followers.Add(userFollowerItem);

        await _unit.SaveChangesAsync();
    }
}
