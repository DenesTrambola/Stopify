using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Contracts.Services;
using Stopify.Domain.Entities;
using Stopify.Exceptions.ValidationExceptions;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Stopify.Domain.Services;

public class SongPlaylistService : ISongPlaylistService
{
    private readonly IUnitOfWork _unit;

    public SongPlaylistService(IUnitOfWork unit) =>
        _unit = unit;

    public async Task CreateAsync(string songTitle, string playlistTitle, int? position = null)
    {
        var playlist = await _unit.Playlists.GetByTitleAsync(playlistTitle);
        if (playlist is null)
            throw new EntityNotFoundException(nameof(playlist));

        var song = await _unit.Songs.GetByTitleAsync(songTitle);
        if (song is null)
            throw new EntityNotFoundException(nameof(song));

        var songPlaylistItem = new SongPlaylist(song.Id, playlist.Id, (position is null ? ++playlist.Songs : (int)position));
        await CreateAsync(songPlaylistItem);

        if (playlist.SongPlaylists.Any(sp => sp.PlaylistId == playlist.Id && sp.SongId == song.Id)
            || song.SongPlaylists.Any(sp => sp.PlaylistId == playlist.Id && sp.SongId == song.Id))
            throw new EntityAlreadyExistsException($"Relationship {nameof(song)} - {nameof(playlist)}");

        playlist.Duration += song.Duration;

        playlist.SongPlaylists.Add(songPlaylistItem);
        song.SongPlaylists.Add(songPlaylistItem);

        _unit.Playlists.Update(playlist, e => e.Songs, e => e.Duration);
        await _unit.SaveChangesAsync();
    }

    public async Task CreateAsync(SongPlaylist entity)
    {
        var song = await _unit.Songs.GetByIdAsync(entity.SongId);
        var playlist = await _unit.Playlists.GetByIdAsync(entity.PlaylistId);

        if (song == null)
            throw new EntityNotFoundException(nameof(song));
        if (playlist == null)
            throw new EntityNotFoundException(nameof(playlist));

        var songPlaylistItem = await GetAsync(song.Id, playlist.Id);
        if (songPlaylistItem is not null)
            throw new EntityAlreadyExistsException(nameof(entity));

        await _unit.SongPlaylists.AddAsync(entity);
        await _unit.SaveChangesAsync();
    }

    public async Task<IEnumerable<SongPlaylist?>> GetAllAsync() =>
        await _unit.SongPlaylists.GetAllAsync();

    public async Task<IEnumerable<SongPlaylist?>> GetAllAsync(Expression<Func<SongPlaylist, bool>>? expression) =>
        await _unit.SongPlaylists.GetAllAsync(expression);

    public async Task<IEnumerable<SongPlaylist?>> GetAllByPlaylistIdAsync(int playlistId, Expression<Func<SongPlaylist, bool>>? expression = null) =>
        await _unit.SongPlaylists.GetAllByPlaylistIdAsync(playlistId, expression);

    public async Task<IEnumerable<SongPlaylist?>> GetAllByPositionAsync(int position, Expression<Func<SongPlaylist, bool>>? expression = null) =>
        await _unit.SongPlaylists.GetAllByPositionAsync(position, expression);

    public async Task<IEnumerable<SongPlaylist?>> GetAllBySongIdAsync(int songId, Expression<Func<SongPlaylist, bool>>? expression = null) =>
        await _unit.SongPlaylists.GetAllBySongIdAsync(songId, expression);

    public async Task<SongPlaylist> GetAsync(int songId, int playlistId, Expression<Func<SongPlaylist, bool>>? expression = null)
    {
        var songPlaylistsBySong = await _unit.SongPlaylists.GetAllBySongIdAsync(songId);
        var songPlaylistsByPlaylist = await _unit.SongPlaylists.GetAllByPlaylistIdAsync(playlistId);

        var commonObjects = songPlaylistsByPlaylist.Intersect(songPlaylistsBySong);
        if (!commonObjects.Any())
            throw new EntityNotFoundException(nameof(SongPlaylist));

        return commonObjects.First()!;
    }

    public async Task<SongPlaylist?> GetFirstByPlaylistIdAsync(int playlistId, Expression<Func<SongPlaylist, bool>>? expression = null) =>
        await _unit.SongPlaylists.GetFirstByPlaylistIdAsync(playlistId, expression);

    public async Task<SongPlaylist?> GetFirstByPositionAsync(int position, Expression<Func<SongPlaylist, bool>>? expression = null) =>
        await _unit.SongPlaylists.GetFirstByPositionAsync(position, expression);

    public async Task<SongPlaylist?> GetFirstBySongIdAsync(int songId, Expression<Func<SongPlaylist, bool>>? expression = null) =>
        await _unit.SongPlaylists.GetFirstBySongIdAsync(songId, expression);

    public async Task RemoveAsync(SongPlaylist dto)
    {
        var song = await _unit.Songs.GetByIdAsync(dto.SongId);
        var playlist = await _unit.Playlists.GetByIdAsync(dto.PlaylistId);

        if (song == null)
            throw new EntityNotFoundException(nameof(song));
        if (playlist == null)
            throw new EntityNotFoundException(nameof(playlist));

        var songPlaylistItem = await GetAsync(song.Id, playlist.Id);
        if (songPlaylistItem is null)
            throw new EntityNotFoundException(nameof(songPlaylistItem));

        _unit.SongPlaylists.Remove(songPlaylistItem);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdatePlaylistAsync(SongPlaylist songPlaylistItem, [StringLength(50, ErrorMessage = "Maximum length is 50!")] string newPlaylistTitle)
    {
        var oldPlaylist = await _unit.Playlists.GetByIdAsync(songPlaylistItem.PlaylistId);
        if (oldPlaylist is null)
            throw new EntityNotFoundException(nameof(oldPlaylist));
        oldPlaylist.Songs--;

        var newPlaylist = await _unit.Playlists.GetByTitleAsync(newPlaylistTitle);
        if (newPlaylist == null)
            throw new EntityNotFoundException(nameof(newPlaylist));
        newPlaylist.Songs++;

        var songPlaylistItemByNewPlaylistTitle = await GetAsync(songPlaylistItem.SongId, newPlaylist.Id);
        if (songPlaylistItemByNewPlaylistTitle != null)
            throw new EntityAlreadyExistsException(nameof(songPlaylistItem));

        songPlaylistItem.PlaylistId = newPlaylist.Id;

        _unit.Playlists.Update(oldPlaylist, e => e.Songs);
        _unit.Playlists.Update(newPlaylist, e => e.Songs);
        _unit.SongPlaylists.Update(songPlaylistItem, e => e.PlaylistId);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdatePositionAsync(SongPlaylist songPlaylistItem, int newPosition)
    {
        if (songPlaylistItem.Position == newPosition)
            throw new SamePropertyNameException(nameof(songPlaylistItem) + " " + nameof(songPlaylistItem.Position));
        songPlaylistItem.Position = newPosition;

        _unit.SongPlaylists.Update(songPlaylistItem, e => e.Position);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateSongAsync(SongPlaylist songPlaylistItem, [StringLength(200, ErrorMessage = "Maximum length is 200!")] string newSongTitle)
    {
        var song = await _unit.Songs.GetByTitleAsync(newSongTitle);
        if (song == null)
            throw new EntityNotFoundException(nameof(song));

        var songPlaylistItemByNewSongTitle = await GetAsync(song.Id, songPlaylistItem.PlaylistId);
        if (songPlaylistItemByNewSongTitle != null)
            throw new EntityAlreadyExistsException(nameof(songPlaylistItem));

        songPlaylistItem.SongId = song.Id;

        _unit.SongPlaylists.Update(songPlaylistItem, e => e.SongId);
        await _unit.SaveChangesAsync();
    }
}
