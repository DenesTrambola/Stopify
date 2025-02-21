using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Contracts.Services;
using Stopify.Domain.Entities;
using Stopify.Exceptions.ValidationExceptions;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Stopify.Domain.Services;

public class UserArtistService : IUserArtistService
{
    private readonly IUnitOfWork _unit;

    public UserArtistService(IUnitOfWork unit) =>
        _unit = unit;

    public async Task CreateAsync(string username, string artistName)
    {
        var artist = await _unit.Artists.GetByNameAsync(artistName);
        if (artist is null)
            throw new EntityNotFoundException(nameof(Artist));

        var user = await _unit.Users.GetByUsernameAsync(username);
        if (user is null)
            throw new EntityNotFoundException(nameof(User));

        var userArtistItem = new UserArtist(user.Id, artist.Id);

        if (artist.UserArtists.Any(ua => ua.ArtistId == artist.Id && ua.UserId == user.Id)
            || user.UserArtists.Any(ua => ua.ArtistId == artist.Id && ua.UserId == user.Id))
            throw new EntityAlreadyExistsException(nameof(UserArtist));

        await _unit.UserArtists.AddAsync(userArtistItem);

        artist.UserArtists.Add(userArtistItem);
        user.UserArtists.Add(userArtistItem);

        await _unit.SaveChangesAsync();
    }

    public async Task CreateAsync(UserArtist entity)
    {
        var user = await _unit.Users.GetByIdAsync(entity.UserId);
        var artist = await _unit.Artists.GetByIdAsync(entity.ArtistId);

        if (user == null)
            throw new EntityNotFoundException(nameof(User));
        if (artist == null)
            throw new EntityNotFoundException(nameof(Artist));

        var userArtistItem = await GetAsync(user.Id, artist.Id);
        if (userArtistItem is not null)
            throw new EntityAlreadyExistsException(nameof(UserArtist));

        await _unit.UserArtists.AddAsync(entity);
        await _unit.SaveChangesAsync();
    }

    public async Task<IEnumerable<UserArtist>?> GetAllAsync() =>
        await _unit.UserArtists.GetAllAsync();

    public async Task<IEnumerable<UserArtist>?> GetAllAsync(Expression<Func<UserArtist, bool>>? expression) =>
        await _unit.UserArtists.GetAllAsync(expression);

    public async Task<IEnumerable<UserArtist>?> GetAllByArtistIdAsync(int artistId, Expression<Func<UserArtist, bool>>? expression = null) =>
        await _unit.UserArtists.GetAllByArtistIdAsync(artistId, expression);

    public async Task<IEnumerable<UserArtist>?> GetAllByFollowedDateAsync(DateTime followedDate, Expression<Func<UserArtist, bool>>? expression = null) =>
        await _unit.UserArtists.GetAllByFollowedDateAsync(followedDate, expression);

    public async Task<IEnumerable<UserArtist>?> GetAllByUserIdAsync(int userId, Expression<Func<UserArtist, bool>>? expression = null) =>
        await _unit.UserArtists.GetAllByUserIdAsync(userId, expression);

    public async Task<UserArtist> GetAsync(int userId, int artistId, Expression<Func<SongPlaylist, bool>>? expression = null)
    {
        var userArtistByUser = await _unit.UserArtists.GetAllByUserIdAsync(userId);
        var userArtistByArtist = await _unit.UserArtists.GetAllByArtistIdAsync(artistId);

        var commonObjects = userArtistByUser.Intersect(userArtistByArtist);
        if (!commonObjects.Any())
            throw new EntityNotFoundException(nameof(UserArtist));

        return commonObjects.First()!;
    }

    public async Task<UserArtist?> GetFirstByArtistIdAsync(int artistId, Expression<Func<UserArtist, bool>>? expression = null) =>
        await _unit.UserArtists.GetFirstByArtistIdAsync(artistId, expression);

    public async Task<UserArtist?> GetFirstByFollowedDateAsync(DateTime followedDate, Expression<Func<UserArtist, bool>>? expression = null) =>
        await _unit.UserArtists.GetFirstByFollowedDateAsync(followedDate, expression);

    public async Task<UserArtist?> GetFirstByUserIdAsync(int userId, Expression<Func<UserArtist, bool>>? expression = null) =>
        await _unit.UserArtists.GetFirstByUserIdAsync(userId, expression);

    public async Task RemoveAsync(UserArtist dto)
    {
        var user = await _unit.Users.GetByIdAsync(dto.UserId);
        var artist = await _unit.Artists.GetByIdAsync(dto.ArtistId);

        if (user == null)
            throw new EntityNotFoundException(nameof(User));
        if (artist == null)
            throw new EntityNotFoundException(nameof(Artist));

        var userArtistItem = await GetAsync(user.Id, artist.Id);
        if (userArtistItem is null)
            throw new EntityNotFoundException(nameof(UserArtist));

        _unit.UserArtists.Remove(userArtistItem);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateArtistAsync(UserArtist userArtistsItem, [StringLength(50, ErrorMessage = "Maximum length is 50!")] string newArtistName)
    {
        var artist = await _unit.Artists.GetByNameAsync(newArtistName);
        if (artist == null)
            throw new EntityNotFoundException(nameof(Artist));

        var userArtistsItemByNewArtistName = await GetAsync(userArtistsItem.UserId, artist.Id);
        if (userArtistsItemByNewArtistName != null)
            throw new EntityAlreadyExistsException(nameof(UserArtist));

        userArtistsItem.ArtistId = artist.Id;

        _unit.UserArtists.Update(userArtistsItem, e => e.ArtistId);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateFollowedDateAsync(UserArtist userArtistsItem, DateTime newFollowedDate)
    {
        if (userArtistsItem.FollowedDate == newFollowedDate)
            throw new SamePropertyNameException(nameof(UserArtist) + " " + nameof(UserArtist.FollowedDate));
        userArtistsItem.FollowedDate = newFollowedDate;

        _unit.UserArtists.Update(userArtistsItem, e => e.FollowedDate);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(UserArtist userArtistsItem, [StringLength(50, ErrorMessage = "Maximum length is 50!")] string newUsername)
    {
        var user = await _unit.Users.GetByUsernameAsync(newUsername);
        if (user == null)
            throw new EntityNotFoundException(nameof(User));

        var userArtistsItemByNewUsername = await GetAsync(user.Id, userArtistsItem.ArtistId);
        if (userArtistsItemByNewUsername != null)
            throw new EntityAlreadyExistsException(nameof(UserArtist));

        userArtistsItem.UserId = user.Id;

        _unit.UserArtists.Update(userArtistsItem, e => e.UserId);
        await _unit.SaveChangesAsync();
    }
}
