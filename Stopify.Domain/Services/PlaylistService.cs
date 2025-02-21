using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Contracts.Services;
using Stopify.Domain.DTOs;
using Stopify.Domain.Entities;
using Stopify.Domain.Other;
using Stopify.Exceptions.ValidationExceptions;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Stopify.Domain.Services;

public class PlaylistService : IPlaylistService
{
    private readonly IUnitOfWork _unit;
    private readonly ISongPlaylistService _songPlaylistService;
    private readonly IUserPlaylistService _userPlaylistService;

    public PlaylistService(IUnitOfWork unit, ISongPlaylistService songPlaylistService, IUserPlaylistService userPlaylistService)
    {
        _unit = unit;
        _songPlaylistService = songPlaylistService;
        _userPlaylistService = userPlaylistService;
    }

    public async Task AddSongAsync(string playlistTitle, string songTitle, int? position = null) =>
        await _songPlaylistService.CreateAsync(songTitle, playlistTitle, position);

    public async Task AddUserAsync(string playlistTitle, string username) =>
        await _userPlaylistService.CreateAsync(username, playlistTitle);

    public async Task CreateAsync(Playlist entity)
    {
        var playlist = await _unit.Playlists.GetByTitleAsync(entity.Title);
        if (playlist != null)
            throw new EntityAlreadyExistsException(nameof(Playlist));

        await _unit.Playlists.AddAsync(entity);
        await _unit.SaveChangesAsync();
    }

    public async Task<IEnumerable<Playlist>?> GetAllAsync() =>
        await _unit.Playlists.GetAllAsync();

    public async Task<IEnumerable<Playlist>?> GetAllAsync(Expression<Func<Playlist, bool>>? expression) =>
        await _unit.Playlists.GetAllAsync(expression);

    public async Task<IEnumerable<Playlist>?> GetAllByCoverAsync(string cover, Expression<Func<Playlist, bool>>? expression = null) =>
        await _unit.Playlists.GetAllByCoverAsync($"{Album.MainCoverPath}{cover}.jpg", expression);

    public async Task<IEnumerable<Playlist>?> GetAllByDescriptionAsync(string description, Expression<Func<Playlist, bool>>? expression = null) =>
        await _unit.Playlists.GetAllByDescriptionAsync(description, expression);

    public async Task<IEnumerable<Playlist>?> GetAllByDurationAsync(int duration, Expression<Func<Playlist, bool>>? expression = null) =>
        await _unit.Playlists.GetAllByDurationAsync(duration, expression);

    public async Task<IEnumerable<Playlist>?> GetAllBySavesAsync(int saves, Expression<Func<Playlist, bool>>? expression = null) =>
        await _unit.Playlists.GetAllBySavesAsync(saves, expression);

    public async Task<IEnumerable<Playlist>?> GetAllBySongsAsync(int songs, Expression<Func<Playlist, bool>>? expression = null) =>
        await _unit.Playlists.GetAllBySongsAsync(songs, expression);

    public async Task<IEnumerable<Playlist>?> GetAllNotPublicAsync(Expression<Func<Playlist, bool>>? expression = null) =>
        await _unit.Playlists.GetAllNotPublicAsync(expression);

    public async Task<IEnumerable<Playlist>?> GetAllPublicAsync(Expression<Func<Playlist, bool>>? expression = null) =>
        await _unit.Playlists.GetAllPublicAsync(expression);

    public async Task<Playlist?> GetByIdAsync(int id, Expression<Func<Playlist, bool>>? expression = null) =>
        await _unit.Playlists.GetByIdAsync(id, expression);

    public async Task<Playlist?> GetFirstByCoverAsync(string cover, Expression<Func<Playlist, bool>>? expression = null) =>
        await _unit.Playlists.GetFirstByCoverAsync($"{Album.MainCoverPath}{cover}.jpg", expression);

    public async Task<Playlist?> GetFirstByDescriptionAsync(string description, Expression<Func<Playlist, bool>>? expression = null) =>
        await _unit.Playlists.GetFirstByDescriptionAsync(description, expression);

    public async Task<Playlist?> GetFirstByDurationAsync(int duration, Expression<Func<Playlist, bool>>? expression = null) =>
        await _unit.Playlists.GetFirstByDurationAsync(duration, expression);

    public async Task<Playlist?> GetFirstBySavesAsync(int saves, Expression<Func<Playlist, bool>>? expression = null) =>
        await _unit.Playlists.GetFirstBySavesAsync(saves, expression);

    public async Task<Playlist?> GetFirstBySongsAsync(int songs, Expression<Func<Playlist, bool>>? expression = null) =>
        await _unit.Playlists.GetFirstBySongsAsync(songs, expression);

    public async Task<Playlist?> GetByTitleAsync(string title, Expression<Func<Playlist, bool>>? expression = null) =>
        await _unit.Playlists.GetByTitleAsync(title, expression);

    public async Task<Playlist?> GetFirstNotPublicAsync(Expression<Func<Playlist, bool>>? expression = null) =>
        await _unit.Playlists.GetFirstNotPublicAsync(expression);

    public async Task<Playlist?> GetFirstPublicAsync(Expression<Func<Playlist, bool>>? expression = null) =>
        await _unit.Playlists.GetFirstPublicAsync(expression);

    public async Task RemoveAsync(PlaylistDto dto)
    {
        var playlist = await _unit.Playlists.GetByTitleAsync(dto.Title);
        if (playlist == null)
            throw new EntityNotFoundException(nameof(Playlist));

        _unit.Playlists.Remove(playlist);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateCoverAsync(string title, [StringLength(2048, ErrorMessage = "Maximum length is 2048!")] string? newCover)
    {
        var playlist = await GetByTitleAsync(title);
        if (playlist == null)
            throw new EntityNotFoundException(nameof(Playlist));

        newCover = UrlValidation.CheckFormat(newCover) ? newCover : $"{Playlist.MainCoverPath}{newCover}.jpg" ?? $"{Playlist.MainCoverPath}playlist-cover-default.jpg";
        if (!UrlValidation.CheckFormat(playlist.Cover))
            throw new InvalidUrlException();

        if (playlist.Cover == newCover)
            throw new SamePropertyNameException(nameof(Playlist) + " " + nameof(Playlist.Cover));

        _unit.Playlists.Update(playlist, e => e.Cover);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateDescriptionAsync(string title, [StringLength(200, ErrorMessage = "Maximum length is 200!")] string newDescription)
    {
        var playlist = await GetByTitleAsync(title);
        if (playlist == null)
            throw new EntityNotFoundException(nameof(Playlist));

        if (playlist.Description == newDescription)
            throw new SamePropertyNameException(nameof(Playlist) + " " + nameof(Playlist.Description));
        playlist.Description = newDescription;

        _unit.Playlists.Update(playlist, e => e.Description);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateDurationAsync(string title, int newDuration)
    {
        var playlist = await GetByTitleAsync(title);
        if (playlist == null)
            throw new EntityNotFoundException(nameof(Playlist));

        if (playlist.Duration == newDuration)
            throw new SamePropertyNameException(nameof(Playlist) + " " + nameof(Playlist.Duration));
        playlist.Duration = newDuration;

        _unit.Playlists.Update(playlist, e => e.Duration);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdatePublicAsync(string title, bool isPublic)
    {
        var playlist = await GetByTitleAsync(title);
        if (playlist == null)
            throw new EntityNotFoundException(nameof(Playlist));

        if (playlist.IsPublic == isPublic)
            throw new SamePropertyNameException(nameof(Playlist) + " " + nameof(Playlist.IsPublic));
        playlist.IsPublic = isPublic;

        _unit.Playlists.Update(playlist, e => e.IsPublic);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateSavesAsync(string title, int newSaves)
    {
        var playlist = await GetByTitleAsync(title);
        if (playlist == null)
            throw new EntityNotFoundException(nameof(Playlist));

        if (playlist.Saves == newSaves)
            throw new SamePropertyNameException(nameof(Playlist) + " " + nameof(Playlist.Saves));
        playlist.Saves = newSaves;

        _unit.Playlists.Update(playlist, e => e.Saves);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateSongPositionAsync(string title, string songTitle, int position)
    {
        var playlist = await _unit.Playlists.GetByTitleAsync(title);
        if (playlist is null)
            throw new EntityNotFoundException(nameof(Playlist));

        var song = await _unit.Songs.GetByTitleAsync(songTitle);
        if (song is null)
            throw new EntityNotFoundException(nameof(Song));

        var songPlaylist = await _songPlaylistService.GetAsync(song.Id, playlist.Id);
        if (songPlaylist is null)
            throw new EntityNotFoundException(nameof(SongPlaylist));

        await _songPlaylistService.UpdatePositionAsync(songPlaylist, position);
    }

    public async Task UpdateSongsAsync(string title, int newSongs)
    {
        var playlist = await GetByTitleAsync(title);
        if (playlist == null)
            throw new EntityNotFoundException(nameof(Playlist));

        if (playlist.Songs == newSongs)
            throw new SamePropertyNameException(nameof(Playlist) + " " + nameof(Playlist.Songs));
        playlist.Songs = newSongs;

        _unit.Playlists.Update(playlist, e => e.Songs);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateTitleAsync(string title, [StringLength(50, ErrorMessage = "Maximum length is 50!")] string newTitle)
    {
        var playlist = await GetByTitleAsync(title);
        if (playlist is null)
            throw new EntityNotFoundException(nameof(Playlist));

        if (playlist.Title == newTitle)
            throw new SamePropertyNameException(nameof(Playlist) + " " + nameof(Playlist.Title));

        var playlistByNewTitle = await _unit.Playlists.GetByTitleAsync(newTitle);
        if (playlistByNewTitle is not null)
            throw new EntityAlreadyExistsException(nameof(Playlist));

        playlist.Title = newTitle;
        _unit.Playlists.Update(playlist, e => e.Title);
        await _unit.SaveChangesAsync();
    }
}
