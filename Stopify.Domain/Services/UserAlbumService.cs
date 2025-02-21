using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Contracts.Services;
using Stopify.Domain.Entities;
using Stopify.Exceptions.ValidationExceptions;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Stopify.Domain.Services;

public class UserAlbumService : IUserAlbumService
{
    private readonly IUnitOfWork _unit;

    public UserAlbumService(IUnitOfWork unit) =>
        _unit = unit;

    public async Task CreateAsync(string username, string albumTitle)
    {
        var album = await _unit.Albums.GetByTitleAsync(albumTitle);
        if (album is null)
            throw new EntityNotFoundException(nameof(Album));

        var user = await _unit.Users.GetByUsernameAsync(username);
        if (user is null)
            throw new EntityNotFoundException(nameof(User));

        var userAlbumItem = new UserAlbum(user.Id, album.Id);
        await _unit.UserAlbums.AddAsync(userAlbumItem);

        if (album.UserAlbums.Any(ua => ua.UserId == user.Id && ua.AlbumId == album.Id)
            || user.UserAlbums.Any(ua => ua.UserId == user.Id && ua.AlbumId == album.Id))
            throw new EntityAlreadyExistsException(nameof(UserAlbum));

        album.Saves++;

        album.UserAlbums.Add(userAlbumItem);
        user.UserAlbums.Add(userAlbumItem);

        _unit.Albums.Update(album, e => e.Saves);
        await _unit.SaveChangesAsync();
    }

    public async Task CreateAsync(UserAlbum entity)
    {
        var user = await _unit.Users.GetByIdAsync(entity.UserId);
        var album = await _unit.Albums.GetByIdAsync(entity.AlbumId);

        if (user == null)
            throw new EntityNotFoundException(nameof(User));
        if (album == null)
            throw new EntityNotFoundException(nameof(Album));

        var userAlbumItem = await GetAsync(user.Id, album.Id);
        if (userAlbumItem is not null)
            throw new EntityAlreadyExistsException(nameof(UserAlbum));

        await _unit.UserAlbums.AddAsync(entity);
        await _unit.SaveChangesAsync();
    }

    public async Task<IEnumerable<UserAlbum>?> GetAllAsync() =>
        await _unit.UserAlbums.GetAllAsync();

    public async Task<IEnumerable<UserAlbum>?> GetAllAsync(Expression<Func<UserAlbum, bool>>? expression) =>
        await _unit.UserAlbums.GetAllAsync(expression);

    public async Task<IEnumerable<UserAlbum>?> GetAllByAlbumIdAsync(int albumId, Expression<Func<UserAlbum, bool>>? expression = null) =>
        await _unit.UserAlbums.GetAllByAlbumIdAsync(albumId, expression);

    public async Task<IEnumerable<UserAlbum>?> GetAllBySavedDateAsync(DateTime savedDate, Expression<Func<UserAlbum, bool>>? expression = null) =>
        await _unit.UserAlbums.GetAllBySavedDateAsync(savedDate, expression);

    public async Task<IEnumerable<UserAlbum>?> GetAllByUserIdAsync(int userId, Expression<Func<UserAlbum, bool>>? expression = null) =>
        await _unit.UserAlbums.GetAllByUserIdAsync(userId, expression);

    public async Task<UserAlbum> GetAsync(int userId, int albumId, Expression<Func<SongPlaylist, bool>>? expression = null)
    {
        var userAlbumsByUser = await _unit.UserAlbums.GetAllByUserIdAsync(userId);
        var userAlbumsByAlbum = await _unit.UserAlbums.GetAllByAlbumIdAsync(albumId);

        var commonObjects = userAlbumsByAlbum.Intersect(userAlbumsByUser);
        if (!commonObjects.Any())
            throw new EntityNotFoundException(nameof(UserAlbum));

        return commonObjects.First()!;
    }

    public async Task<UserAlbum?> GetFirstByAlbumIdAsync(int albumId, Expression<Func<UserAlbum, bool>>? expression = null) =>
        await _unit.UserAlbums.GetFirstByAlbumIdAsync(albumId, expression);

    public async Task<UserAlbum?> GetFirstBySaveDateAsync(DateTime savedDate, Expression<Func<UserAlbum, bool>>? expression = null) =>
        await _unit.UserAlbums.GetFirstBySaveDateAsync(savedDate, expression);

    public async Task<UserAlbum?> GetFirstByUserIdAsync(int userId, Expression<Func<UserAlbum, bool>>? expression = null) =>
        await _unit.UserAlbums.GetFirstByUserIdAsync(userId, expression);

    public async Task RemoveAsync(UserAlbum dto)
    {
        var user = await _unit.Users.GetByIdAsync(dto.UserId);
        var album = await _unit.Albums.GetByIdAsync(dto.AlbumId);

        if (user == null)
            throw new EntityNotFoundException(nameof(User));
        if (album == null)
            throw new EntityNotFoundException(nameof(Album));

        var userAlbumItem = await GetAsync(user.Id, album.Id);
        if (userAlbumItem is null)
            throw new EntityNotFoundException(nameof(UserAlbum));

        _unit.UserAlbums.Remove(userAlbumItem);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateAlbumAsync(UserAlbum userAlbumsItem, [StringLength(50, ErrorMessage = "Maximum length is 50!")] string newAlbumTitle)
    {
        var album = await _unit.Albums.GetByTitleAsync(newAlbumTitle);
        if (album == null)
            throw new EntityNotFoundException(nameof(Album));

        var userAlbumsItemByNewAlbumTitle = await GetAsync(userAlbumsItem.UserId, album.Id);
        if (userAlbumsItemByNewAlbumTitle != null)
            throw new EntityAlreadyExistsException(nameof(UserAlbum));

        userAlbumsItem.AlbumId = album.Id;

        _unit.UserAlbums.Update(userAlbumsItem, e => e.AlbumId);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateSavedDateAsync(UserAlbum userAlbumsItem, DateTime newSavedDate)
    {
        if (userAlbumsItem.SavedDate == newSavedDate)
            throw new SamePropertyNameException(nameof(UserAlbum) + " " + nameof(UserAlbum.SavedDate));
        userAlbumsItem.SavedDate = newSavedDate;

        _unit.UserAlbums.Update(userAlbumsItem, e => e.SavedDate);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(UserAlbum userAlbumsItem, [StringLength(50, ErrorMessage = "Maximum length is 50!")] string newUsername)
    {
        var user = await _unit.Users.GetByUsernameAsync(newUsername);
        if (user == null)
            throw new EntityNotFoundException(nameof(User));

        var userAlbumsItemByNewUsername = await GetAsync(user.Id, userAlbumsItem.AlbumId);
        if (userAlbumsItemByNewUsername != null)
            throw new EntityAlreadyExistsException(nameof(UserAlbum));

        userAlbumsItem.UserId = user.Id;

        _unit.UserAlbums.Update(userAlbumsItem, e => e.UserId);
        await _unit.SaveChangesAsync();
    }
}
