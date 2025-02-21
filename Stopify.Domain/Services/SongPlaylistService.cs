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
            throw new EntityNotFoundException(nameof(Playlist));

        var song = await _unit.Songs.GetByTitleAsync(songTitle);
        if (song is null)
            throw new EntityNotFoundException(nameof(Song));

        var songPlaylistItem = new SongPlaylist(song.Id, playlist.Id, (position is null ? ++playlist.Songs : position.Value));

        await _unit.SongPlaylists.AddAsync(songPlaylistItem);

        if (playlist.SongPlaylists.Any(sp => sp.PlaylistId == playlist.Id && sp.SongId == song.Id && sp.Position == (position ?? 0))
            || song.SongPlaylists.Any(sp => sp.PlaylistId == playlist.Id && sp.SongId == song.Id && sp.Position == (position ?? 0)))
            throw new EntityAlreadyExistsException($"Relationship {nameof(Song)} - {nameof(Playlist)}");

        playlist.Duration += song.Duration;

        playlist.SongPlaylists.Add(songPlaylistItem);
        song.SongPlaylists.Add(songPlaylistItem);

        _unit.Playlists.Update(playlist, e => e.Songs, e => e.Duration);
        await _unit.SaveChangesAsync();
    }

    /// <summary>
    /// This overloaded method is a bit slower than the other method with parameters 'songTitle', 'playlistTitle' and 'position'.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="EntityNotFoundException"></exception>
    /// <exception cref="EntityAlreadyExistsException"></exception>
    public async Task CreateAsync(SongPlaylist entity)
    {
        var song = await _unit.Songs.GetByIdAsync(entity.SongId);
        if (song == null)
            throw new EntityNotFoundException(nameof(Song));

        var playlist = await _unit.Playlists.GetByIdAsync(entity.PlaylistId);
        if (playlist == null)
            throw new EntityNotFoundException(nameof(Playlist));

        var songPlaylistItem = await GetAsync(song.Id, playlist.Id);
        if (songPlaylistItem is not null)
            throw new EntityAlreadyExistsException(nameof(SongPlaylist));

        await CreateAsync(song.Title, playlist.Title, entity.Position);
    }

    public async Task<IEnumerable<SongPlaylist>?> GetAllAsync() =>
        await _unit.SongPlaylists.GetAllAsync();

    public async Task<IEnumerable<SongPlaylist>?> GetAllAsync(Expression<Func<SongPlaylist, bool>>? expression) =>
        await _unit.SongPlaylists.GetAllAsync(expression);

    public async Task<IEnumerable<SongPlaylist>?> GetAllByPlaylistIdAsync(int playlistId, Expression<Func<SongPlaylist, bool>>? expression = null) =>
        await _unit.SongPlaylists.GetAllByPlaylistIdAsync(playlistId, expression);

    public async Task<IEnumerable<SongPlaylist>?> GetAllByPositionAsync(int position, Expression<Func<SongPlaylist, bool>>? expression = null) =>
        await _unit.SongPlaylists.GetAllByPositionAsync(position, expression);

    public async Task<IEnumerable<SongPlaylist>?> GetAllBySongIdAsync(int songId, Expression<Func<SongPlaylist, bool>>? expression = null) =>
        await _unit.SongPlaylists.GetAllBySongIdAsync(songId, expression);

    public async Task<SongPlaylist> GetAsync(int songId, int playlistId, Expression<Func<SongPlaylist, bool>>? expression = null)
    {
        var songPlaylistsBySong = await _unit.SongPlaylists.GetAllBySongIdAsync(songId);
        if (songPlaylistsBySong is null)
            throw new EntityNotFoundException(nameof(SongPlaylist));

        var songPlaylistsByPlaylist = await _unit.SongPlaylists.GetAllByPlaylistIdAsync(playlistId);
        if (songPlaylistsByPlaylist is null)
            throw new EntityNotFoundException(nameof(SongPlaylist));

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
            throw new EntityNotFoundException(nameof(Song));
        if (playlist == null)
            throw new EntityNotFoundException(nameof(Playlist));

        var songPlaylistItem = await GetAsync(song.Id, playlist.Id);
        if (songPlaylistItem is null)
            throw new EntityNotFoundException(nameof(SongPlaylist));

        _unit.SongPlaylists.Remove(songPlaylistItem);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdatePlaylistAsync(SongPlaylist songPlaylistItem,
                                      [StringLength(50, ErrorMessage = "Maximum length is 50!")] string newPlaylistTitle,
                                      int? position = null)
    {
        var song = await _unit.Songs.GetByIdAsync(songPlaylistItem.SongId);
        if (song is null)
            throw new EntityNotFoundException(nameof(Song));

        var oldPlaylist = await _unit.Playlists.GetByIdAsync(songPlaylistItem.PlaylistId);
        if (oldPlaylist is null)
            throw new EntityNotFoundException(nameof(Playlist));
        oldPlaylist.Songs--;

        var songPlaylistsByOldPlaylist = await _unit.SongPlaylists.GetAllByPlaylistIdAsync(oldPlaylist.Id);
        if (songPlaylistsByOldPlaylist is null)
            throw new EntityNotFoundException(nameof(SongPlaylist));

        foreach (var sp in songPlaylistsByOldPlaylist.Where(sp => sp.Position > songPlaylistItem.Position))
        {
            sp.Position--;
            _unit.SongPlaylists.Update(sp, sp => sp.Position);
        }

        var newPlaylist = await _unit.Playlists.GetByTitleAsync(newPlaylistTitle);
        if (newPlaylist == null)
            throw new EntityNotFoundException(nameof(Playlist));

        var songPlaylistItemByNewPlaylistTitle = await GetAsync(songPlaylistItem.SongId, newPlaylist.Id);
        if (songPlaylistItemByNewPlaylistTitle is not null)
            throw new EntityAlreadyExistsException(nameof(songPlaylistItem));

        var songPlaylistsByNewPlaylist = await _unit.SongPlaylists.GetAllByPlaylistIdAsync(newPlaylist.Id);
        if (songPlaylistsByNewPlaylist == null)
            throw new EntityNotFoundException(nameof(SongPlaylist));

        if (position is not null)
        {
            foreach (var sp in songPlaylistsByNewPlaylist.Where(sp => sp.Position >= position))
            {
                sp.Position++;
                _unit.SongPlaylists.Update(sp, sp => sp.Position);
            }
        }
        else
            position = newPlaylist.Songs + 1;

        songPlaylistItem.PlaylistId = newPlaylist.Id;
        songPlaylistItem.Position = position.Value;

        newPlaylist.Songs++;
        _unit.Playlists.Update(oldPlaylist, e => e.Songs);
        _unit.Playlists.Update(newPlaylist, e => e.Songs);
        _unit.SongPlaylists.Update(songPlaylistItem, e => e.PlaylistId, e => e.Position);

        await _unit.SaveChangesAsync();
    }

    public async Task UpdatePositionAsync(SongPlaylist songPlaylistItem, int newPosition)
    {
        if (songPlaylistItem.Position == newPosition)
            throw new SamePropertyNameException($"{nameof(SongPlaylist)} {nameof(SongPlaylist.Position)}");

        var songPlaylistsByPlaylist = await _unit.SongPlaylists.GetAllByPlaylistIdAsync(songPlaylistItem.PlaylistId);
        if (songPlaylistsByPlaylist is null)
            throw new EntityNotFoundException(nameof(SongPlaylist));

        int oldPosition = songPlaylistItem.Position;
        if (newPosition < oldPosition)
        {
            foreach (var sp in songPlaylistsByPlaylist.Where(sp => sp.Position >= newPosition && sp.Position < oldPosition))
            {
                sp.Position++;
                _unit.SongPlaylists.Update(sp, sp => sp.Position);
            }
        }
        else
        {
            foreach (var sp in songPlaylistsByPlaylist.Where(sp => sp.Position <= newPosition && sp.Position > oldPosition))
            {
                sp.Position--;
                _unit.SongPlaylists.Update(sp, sp => sp.Position);
            }
        }

        songPlaylistItem.Position = newPosition;

        _unit.SongPlaylists.Update(songPlaylistItem, e => e.Position);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateSongAsync(SongPlaylist songPlaylistItem, [StringLength(200, ErrorMessage = "Maximum length is 200!")] string newSongTitle)
    {
        var song = await _unit.Songs.GetByTitleAsync(newSongTitle);
        if (song == null)
            throw new EntityNotFoundException(nameof(Song));

        var songPlaylistItemByNewSongTitle = await GetAsync(song.Id, songPlaylistItem.PlaylistId);
        if (songPlaylistItemByNewSongTitle != null)
            throw new EntityAlreadyExistsException(nameof(SongPlaylist));

        songPlaylistItem.SongId = song.Id;

        _unit.SongPlaylists.Update(songPlaylistItem, e => e.SongId);
        await _unit.SaveChangesAsync();
    }
}
