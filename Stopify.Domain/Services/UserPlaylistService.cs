using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Contracts.Services;
using Stopify.Domain.Entities;
using Stopify.Exceptions.ValidationExceptions;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Stopify.Domain.Services;

public class UserPlaylistService : IUserPlaylistService
{
    private readonly IUnitOfWork _unit;

    public UserPlaylistService(IUnitOfWork unit) =>
        _unit = unit;

    public async Task CreateAsync(string username, string playlistTitle)
    {
        var playlist = await _unit.Playlists.GetByTitleAsync(playlistTitle);
        if (playlist is null)
            throw new EntityNotFoundException(nameof(Playlist));

        var user = await _unit.Users.GetByUsernameAsync(username);
        if (user is null)
            throw new EntityNotFoundException(nameof(User));

        var userPlaylistItem = new UserPlaylist(user.Id, playlist.Id);

        if (playlist.UserPlaylists.Any(up => up.PlaylistId == playlist.Id && up.UserId == user.Id)
            || user.UserPlaylists.Any(up => up.PlaylistId == playlist.Id && up.UserId == user.Id))
            throw new EntityAlreadyExistsException(nameof(UserPlaylist));

        playlist.Songs++;

        playlist.UserPlaylists.Add(userPlaylistItem);
        user.UserPlaylists.Add(userPlaylistItem);
        await _unit.UserPlaylists.AddAsync(userPlaylistItem);

        _unit.Playlists.Update(playlist, e => e.Songs);
        await _unit.SaveChangesAsync();
    }

    public async Task CreateAsync(UserPlaylist entity)
    {
        var user = await _unit.Users.GetByIdAsync(entity.UserId);
        var playlist = await _unit.Playlists.GetByIdAsync(entity.PlaylistId);

        if (user == null)
            throw new EntityNotFoundException(nameof(User));
        if (playlist == null)
            throw new EntityNotFoundException(nameof(Playlist));

        var userPlaylistItem = await GetAsync(user.Id, playlist.Id);
        if (userPlaylistItem is not null)
            throw new EntityAlreadyExistsException(nameof(UserPlaylist));

        playlist.Saves++;
        await _unit.UserPlaylists.AddAsync(entity);

        _unit.Playlists.Update(playlist, e => e.Saves);
        await _unit.SaveChangesAsync();
    }

    public async Task<IEnumerable<UserPlaylist>?> GetAllAsync() =>
        await _unit.UserPlaylists.GetAllAsync();

    public async Task<IEnumerable<UserPlaylist>?> GetAllAsync(Expression<Func<UserPlaylist, bool>>? expression) =>
        await _unit.UserPlaylists.GetAllAsync(expression);

    public async Task<IEnumerable<UserPlaylist>?> GetAllByPlaylistIdAsync(int playlistId, Expression<Func<UserPlaylist, bool>>? expression = null) =>
        await _unit.UserPlaylists.GetAllByPlaylistIdAsync(playlistId, expression);

    public async Task<IEnumerable<UserPlaylist>?> GetAllBySavedDateAsync(DateTime savedDate, Expression<Func<UserPlaylist, bool>>? expression = null) =>
        await _unit.UserPlaylists.GetAllBySavedDateAsync(savedDate, expression);

    public async Task<IEnumerable<UserPlaylist>?> GetAllByUserIdAsync(int userId, Expression<Func<UserPlaylist, bool>>? expression = null) =>
        await _unit.UserPlaylists.GetAllByUserIdAsync(userId, expression);

    public async Task<UserPlaylist> GetAsync(int userId, int playlistId, Expression<Func<SongPlaylist, bool>>? expression = null)
    {
        var userPlaylistByUser = await _unit.UserPlaylists.GetAllByUserIdAsync(playlistId);
        var userPlaylistByPlaylist = await _unit.UserPlaylists.GetAllByPlaylistIdAsync(userId);

        var commonObjects = userPlaylistByUser.Intersect(userPlaylistByPlaylist);
        if (!commonObjects.Any())
            throw new EntityNotFoundException(nameof(UserPlaylist));

        return commonObjects.First()!;
    }

    public async Task<UserPlaylist?> GetFirstByPlaylistIdAsync(int playlistId, Expression<Func<UserPlaylist, bool>>? expression = null) =>
        await _unit.UserPlaylists.GetFirstByPlaylistIdAsync(playlistId, expression);

    public async Task<UserPlaylist?> GetFirstBySavedDateAsync(DateTime savedDate, Expression<Func<UserPlaylist, bool>>? expression = null) =>
        await _unit.UserPlaylists.GetFirstBySavedDateAsync(savedDate, expression);

    public async Task<UserPlaylist?> GetFirstByUserIdAsync(int userId, Expression<Func<UserPlaylist, bool>>? expression = null) =>
        await _unit.UserPlaylists.GetFirstByUserIdAsync(userId, expression);

    public async Task RemoveAsync(UserPlaylist dto)
    {
        var user = await _unit.Users.GetByIdAsync(dto.UserId);
        var playlist = await _unit.Playlists.GetByIdAsync(dto.PlaylistId);

        if (user == null)
            throw new EntityNotFoundException(nameof(User));
        if (playlist == null)
            throw new EntityNotFoundException(nameof(Playlist));

        var userPlaylistItem = await GetAsync(user.Id, playlist.Id);
        throw new EntityNotFoundException(nameof(UserPlaylist));

        _unit.UserPlaylists.Remove(userPlaylistItem);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdatePlaylistAsync(UserPlaylist userPlaylistsItem, [StringLength(50, ErrorMessage = "Maximum length is 50!")] string newPlaylistTitle)
    {
        var playlist = await _unit.Playlists.GetByTitleAsync(newPlaylistTitle);
        if (playlist == null)
            throw new EntityNotFoundException(nameof(Playlist));

        var userPlaylistsItemByNewPlaylistTitle = await GetAsync(userPlaylistsItem.UserId, playlist.Id);
        if (userPlaylistsItemByNewPlaylistTitle != null)
            throw new EntityAlreadyExistsException(nameof(UserPlaylist));

        userPlaylistsItem.PlaylistId = playlist.Id;

        _unit.UserPlaylists.Update(userPlaylistsItem, e => e.PlaylistId);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateSavedDateAsync(UserPlaylist userPlaylistsItem, DateTime newSavedDate)
    {
        if (userPlaylistsItem.SavedDate == newSavedDate)
            throw new SamePropertyNameException(nameof(UserPlaylist) + " " + nameof(UserPlaylist.SavedDate));
        userPlaylistsItem.SavedDate = newSavedDate;

        _unit.UserPlaylists.Update(userPlaylistsItem, e => e.SavedDate);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(UserPlaylist userPlaylistsItem, [StringLength(50, ErrorMessage = "Maximum length is 50!")] string newUsername)
    {
        var user = await _unit.Users.GetByUsernameAsync(newUsername);
        if (user == null)
            throw new EntityNotFoundException(nameof(User));

        var userPlaylistsItemByNewUsername = await GetAsync(user.Id, userPlaylistsItem.PlaylistId);
        if (userPlaylistsItemByNewUsername != null)
            throw new EntityAlreadyExistsException(nameof(UserPlaylist));

        userPlaylistsItem.UserId = user.Id;

        _unit.UserPlaylists.Update(userPlaylistsItem, e => e.UserId);
        await _unit.SaveChangesAsync();
    }
}
