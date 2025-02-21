using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Contracts.Services;
using Stopify.Domain.DTOs;
using Stopify.Domain.Entities;
using Stopify.Domain.Other;
using Stopify.Exceptions.ValidationExceptions;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Stopify.Domain.Services;

public class SongService : ISongService
{
    private readonly IUnitOfWork _unit;
    private readonly ISongPlaylistService _songPlaylistService;
    private readonly IPlaybackHistoryService _playbackHistoryService;
    private readonly IQueueService _queueService;
    private readonly IRecentPlayedService _recentPlayedService;
    private readonly ISongArtistService _songArtistService;
    private readonly ISongGenreService _songGenreService;
    private readonly ISongAlbumService _songAlbumService;

    public SongService(IUnitOfWork unit,
                       ISongPlaylistService songPlaylistService,
                       IPlaybackHistoryService playbackHistoryService,
                       IQueueService queueService,
                       IRecentPlayedService recentPlayedService,
                       ISongArtistService songArtistService,
                       ISongGenreService songGenreService,
                       ISongAlbumService songAlbumService)
    {
        _unit = unit;
        _songPlaylistService = songPlaylistService;
        _playbackHistoryService = playbackHistoryService;
        _queueService = queueService;
        _recentPlayedService = recentPlayedService;
        _songArtistService = songArtistService;
        _songGenreService = songGenreService;
        _songAlbumService = songAlbumService;
    }

    public async Task AddArtistAsync(string songTitle, string artistName) =>
        await _songArtistService.CreateAsync(songTitle, artistName);

    public async Task AddGenreAsync(string songTitle, string genreName) =>
        await _songGenreService.CreateAsync(songTitle, genreName);

    public async Task AddPlaybackHistoryAsync(string songTitle, string username, int? position = null)
    {
        var song = await _unit.Songs.GetByTitleAsync(songTitle);
        if (song is null)
            throw new EntityNotFoundException(nameof(Song));

        var user = await _unit.Users.GetByUsernameAsync(username);
        if (user is null)
            throw new EntityNotFoundException(nameof(User));

        var historyItem = new PlaybackHistory(user.Id, song.Id, (position is null ? user.PlaybackHistories.Count + 1 : position.Value));
        await _playbackHistoryService.CreateAsync(historyItem);

        song.PlaybackHistories.Add(historyItem);
        user.PlaybackHistories.Add(historyItem);
    }

    public async Task AddPlaylistAsync(string songTitle, string playlistTitle, int? position = null) =>
        await _songPlaylistService.CreateAsync(songTitle, playlistTitle, position);

    public async Task AddQueueAsync(string songTitle, string username, int? position = null)
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

    public async Task AddRecentPlayedAsync(string songTitle, string username, int? position = null)
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

    public async Task CreateAsync(Song entity)
    {
        var song = await _unit.Songs.GetByTitleAsync(entity.Title);
        if (song != null)
            throw new EntityAlreadyExistsException(nameof(Song));

        await _unit.Songs.AddAsync(entity);
        await _unit.SaveChangesAsync();
    }

    public async Task<IEnumerable<Song?>?> GetAllAsync() =>
        await _unit.Songs.GetAllAsync();

    public async Task<IEnumerable<Song>?> GetAllAsync(Expression<Func<Song, bool>>? expression) =>
        await _unit.Songs.GetAllAsync(expression);

    public async Task<IEnumerable<Song>?> GetAllByAlbumIdAsync(int albumId, Expression<Func<Song, bool>>? expression = null) =>
        await _unit.Songs.GetAllByAlbumIdAsync(albumId, expression);

    public async Task<IEnumerable<Song>?> GetAllByCoverAsync(string cover, Expression<Func<Song, bool>>? expression = null) =>
        await _unit.Songs.GetAllByCoverAsync(cover, expression);

    public async Task<IEnumerable<Song>?> GetAllByDurationAsync(int duration, Expression<Func<Song, bool>>? expression = null) =>
        await _unit.Songs.GetAllByDurationAsync(duration, expression);

    public async Task<IEnumerable<Song>?> GetAllByPathAsync(string path, Expression<Func<Song, bool>>? expression = null) =>
        await _unit.Songs.GetAllByPathAsync(path, expression);

    public async Task<IEnumerable<Song>?> GetAllByReleaseDateAsync(DateOnly releaseDate, Expression<Func<Song, bool>>? expression = null) =>
        await _unit.Songs.GetAllByReleaseDateAsync(releaseDate, expression);

    public async Task<Song?> GetByIdAsync(int id, Expression<Func<Song, bool>>? expression = null) =>
        await _unit.Songs.GetByIdAsync(id, expression);

    public async Task<Song?> GetFirstByAlbumIdAsync(int albumId, Expression<Func<Song, bool>>? expression = null) =>
        await _unit.Songs.GetFirstByAlbumIdAsync(albumId, expression);

    public async Task<Song?> GetFirstByCoverAsync(string cover, Expression<Func<Song, bool>>? expression = null) =>
        await _unit.Songs.GetFirstByCoverAsync(cover, expression);

    public async Task<Song?> GetFirstByDurationAsync(int duration, Expression<Func<Song, bool>>? expression = null) =>
        await _unit.Songs.GetFirstByDurationAsync(duration, expression);

    public async Task<Song?> GetFirstByPathAsync(string path, Expression<Func<Song, bool>>? expression = null) =>
        await _unit.Songs.GetFirstByPathAsync(path, expression);

    public async Task<Song?> GetFirstByReleaseDateAsync(DateOnly releaseDate, Expression<Func<Song, bool>>? expression = null) =>
        await _unit.Songs.GetFirstByReleaseDateAsync(releaseDate, expression);

    public async Task<Song?> GetByTitleAsync(string title, Expression<Func<Song, bool>>? expression = null) =>
        await _unit.Songs.GetByTitleAsync(title, expression);

    public async Task RemoveAsync(SongDto dto)
    {
        var song = await _unit.Songs.GetByTitleAsync(dto.Title);
        if (song == null)
            throw new EntityNotFoundException(nameof(Song));

        _unit.Songs.Remove(song);

        var albums = await _unit.Albums.GetAllAsync(a => a.SongsNavigation.Any(s => s.Title == song.Title));
        if (albums is not null)
        {
            foreach (var album in albums)
            {
                album.Songs--;
                album.Duration -= song.Duration;
                _unit.Albums.Update(album, a => a.Songs, a => a.Duration);
            }
        }

        var songPlaylists = await _unit.SongPlaylists.GetAllBySongIdAsync(song.Id);
        if (songPlaylists is not null)
        {
            foreach (var songPlaylist in songPlaylists)
            {
                var playlist = await _unit.Playlists.GetByIdAsync(songPlaylist.PlaylistId);
                playlist.Songs--;
                playlist.Duration -= song.Duration;
                _unit.Playlists.Update(playlist, p => p.Songs, p => p.Duration);
            }
        }

        await _unit.SaveChangesAsync();
    }

    public async Task UpdateAlbumAsync(string title, [StringLength(50, ErrorMessage = "Maximum length is 50!")] string? newAlbumTitle, int? position = null) =>
        await _songAlbumService.UpdateAsync(title, newAlbumTitle, position);

    public async Task UpdateCoverAsync(string title, [StringLength(2048, ErrorMessage = "Maximum length is 2048!")] string? newCover)
    {
        var song = await GetByTitleAsync(title);
        if (song == null)
            throw new EntityNotFoundException(nameof(Song));

        newCover = UrlValidation.CheckFormat(newCover) ? newCover : $"{Song.MainCoverPath}{newCover}.jpg"
            ?? (song.Album is null ? Song.MainCoverPath + "song-cover-default.jpg" : song.Album.Cover);
        if (!UrlValidation.CheckFormat(song.Cover))
            throw new InvalidUrlException();

        if (song.Cover == newCover)
            throw new SamePropertyNameException(nameof(Song) + " " + nameof(Song.Cover));
        song.Cover = newCover;

        _unit.Songs.Update(song, e => e.Cover);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateDurationAsync(string title, int newDuration)
    {
        var song = await GetByTitleAsync(title);
        if (song == null)
            throw new EntityNotFoundException(nameof(Song));

        if (song.Duration == newDuration)
            throw new SamePropertyNameException(nameof(Song) + " " + nameof(Song.Duration));
        song.Duration = newDuration;

        _unit.Songs.Update(song, e => e.Duration);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdatePathAsync(string title, [StringLength(2048, ErrorMessage = "Maximum length is 2048!")] string newPath)
    {
        var song = await GetByTitleAsync(title);
        if (song == null)
            throw new EntityNotFoundException(nameof(Song));

        newPath = $"{Song.MainSongPath}{newPath}.mp3";
        if (!UrlValidation.CheckFormat(song.Path))
            throw new InvalidUrlException();

        if (song.Path == newPath)
            throw new SamePropertyNameException(nameof(Song) + " " + nameof(Song.Cover));
        song.Path = newPath;

        _unit.Songs.Update(song, e => e.Path);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateReleaseDateAsync(string title, DateOnly newReleaseDate)
    {
        var song = await GetByTitleAsync(title);
        if (song == null)
            throw new EntityNotFoundException(nameof(Song));

        if (song.ReleaseDate == newReleaseDate)
            throw new SamePropertyNameException(nameof(Song) + " " + nameof(Song.ReleaseDate));
        song.ReleaseDate = newReleaseDate;

        _unit.Songs.Update(song, e => e.ReleaseDate);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateTitleAsync(string title, [StringLength(200, ErrorMessage = "Maximum length is 200!")] string newTitle)
    {
        var song = await GetByTitleAsync(title);
        if (song == null)
            throw new EntityNotFoundException(nameof(Song));

        var songByNewTitle = await _unit.Songs.GetByTitleAsync(newTitle);
        if (songByNewTitle != null)
            throw new EntityAlreadyExistsException(nameof(Song));

        if (song.Title == newTitle)
            throw new SamePropertyNameException(nameof(Song) + " " + nameof(Song.Title));
        song.Title = newTitle;

        _unit.Songs.Update(song, e => e.Title);
        await _unit.SaveChangesAsync();
    }
}
