using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Contracts.Services;
using Stopify.Domain.DTOs;
using Stopify.Domain.Entities;
using Stopify.Domain.Other;
using Stopify.Exceptions.ValidationExceptions;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Stopify.Domain.Services;

public class AlbumService : IAlbumService
{
    private readonly IUnitOfWork _unit;
    private readonly IUserAlbumService _userAlbumService;
    private readonly IAlbumArtistService _albumArtistService;
    private readonly ISongService _songService;
    private readonly ISongAlbumService _songAlbumService;

    public AlbumService(IUnitOfWork unit,
        IUserAlbumService userAlbumService,
        IAlbumArtistService albumArtistService,
        ISongService songService,
        ISongAlbumService songAlbumService)
    {
        _unit = unit;
        _userAlbumService = userAlbumService;
        _albumArtistService = albumArtistService;
        _songService = songService;
        _songAlbumService = songAlbumService;
    }

    public async Task CreateAsync(Album entity)
    {
        var album = await _unit.Albums.GetByTitleAsync(entity.Title);
        if (album != null)
            throw new EntityAlreadyExistsException(nameof(Album));

        await _unit.Albums.AddAsync(entity);
        await _unit.SaveChangesAsync();
    }

    public async Task<IEnumerable<Album>?> GetAllAsync(Expression<Func<Album, bool>>? expression = null) =>
        await _unit.Albums.GetAllAsync(expression);

    public async Task<IEnumerable<Album>?> GetAllByCoverAsync(string cover, Expression<Func<Album, bool>>? expression = null) =>
        await _unit.Albums.GetAllByCoverAsync($"{Album.MainCoverPath}{cover}.jpg", expression);

    public async Task<IEnumerable<Album>?> GetAllByDurationAsync(int duration, Expression<Func<Album, bool>>? expression = null) =>
        await _unit.Albums.GetAllByDurationAsync(duration, expression);

    public async Task<IEnumerable<Album>?> GetAllByReleaseDateAsync(DateOnly releaseDate, Expression<Func<Album, bool>>? expression = null) =>
        await _unit.Albums.GetAllByReleaseDateAsync(releaseDate, expression);

    public async Task<IEnumerable<Album>?> GetAllBySavesAsync(int saves, Expression<Func<Album, bool>>? expression = null) =>
        await _unit.Albums.GetAllBySavesAsync(saves, expression);

    public async Task<IEnumerable<Album>?> GetAllBySongsAsync(int songs, Expression<Func<Album, bool>>? expression = null) =>
        await _unit.Albums.GetAllBySongsAsync(songs, expression);

    public async Task<Album?> GetByIdAsync(int id, Expression<Func<Album, bool>>? expression = null) =>
        await _unit.Albums.GetByIdAsync(id, expression);

    public async Task<Album?> GetFirstByCoverAsync(string cover, Expression<Func<Album, bool>>? expression = null) =>
        await _unit.Albums.GetFirstByCoverAsync($"{Album.MainCoverPath}{cover}.jpg", expression);

    public async Task<Album?> GetFirstByDurationAsync(int duration, Expression<Func<Album, bool>>? expression = null) =>
        await _unit.Albums.GetFirstByDurationAsync(duration, expression);

    public async Task<Album?> GetFirstByReleaseDateAsync(DateOnly releaseDate, Expression<Func<Album, bool>>? expression = null) =>
        await _unit.Albums.GetFirstByReleaseDateAsync(releaseDate, expression);

    public async Task<Album?> GetFirstBySavesAsync(int saves, Expression<Func<Album, bool>>? expression = null) =>
        await _unit.Albums.GetFirstBySavesAsync(saves, expression);

    public async Task<Album?> GetFirstBySongsAsync(int songs, Expression<Func<Album, bool>>? expression = null) =>
        await _unit.Albums.GetFirstBySongsAsync(songs, expression);

    public async Task<Album?> GetByTitleAsync(string title, Expression<Func<Album, bool>>? expression = null) =>
        await _unit.Albums.GetByTitleAsync(title, expression);

    public async Task RemoveAsync(AlbumDto dto)
    {
        var album = await _unit.Albums.GetByTitleAsync(dto.Title);
        if (album == null)
            throw new EntityNotFoundException(nameof(Album));

        var songs = await _unit.Songs.GetAllByAlbumIdAsync(album.Id);
        if (songs is not null)
        {
            foreach (var song in songs)
            {
                await _songService.UpdateCoverAsync(song.Title, "song-cover-default");
                await _songService.UpdateAlbumAsync(song.Title, null);
            }
        }

        _unit.Albums.Remove(album);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateCoverAsync(string title, [StringLength(2048, ErrorMessage = "Maximum length is 2048!")] string newCover)
    {
        var album = await GetByTitleAsync(title);
        if (album == null)
            throw new EntityNotFoundException(nameof(Album));

        newCover = UrlValidation.CheckFormat(newCover) ? newCover : $"{Album.MainCoverPath}{newCover}.jpg";
        if (!UrlValidation.CheckFormat(album.Cover))
            throw new InvalidUrlException();

        if (album.Cover == newCover)
            throw new SamePropertyNameException(nameof(Album.Cover));
        album.Cover = newCover;

        _unit.Albums.Update(album, e => e.Cover);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateDurationAsync(string title, int newDuration)
    {
        var album = await GetByTitleAsync(title);
        if (album == null)
            throw new EntityNotFoundException(nameof(Album));

        if (album.Duration == newDuration)
            throw new SamePropertyNameException(nameof(Album.Duration));
        album.Duration = newDuration;

        _unit.Albums.Update(album, e => e.Duration);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateReleaseDateAsync(string title, DateOnly newReleaseDate)
    {
        var album = await GetByTitleAsync(title);
        if (album == null)
            throw new EntityNotFoundException(nameof(Album));

        if (album.ReleaseDate == newReleaseDate)
            throw new SamePropertyNameException(nameof(Album.ReleaseDate));
        album.ReleaseDate = newReleaseDate;

        _unit.Albums.Update(album, e => e.ReleaseDate);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateSavesAsync(string title, int newSaves)
    {
        var album = await GetByTitleAsync(title);
        if (album == null)
            throw new EntityNotFoundException(nameof(Album));

        if (album.Saves == newSaves)
            throw new SamePropertyNameException(nameof(Album.Saves));
        album.Saves = newSaves;

        _unit.Albums.Update(album, e => e.Saves);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateSongsAsync(string title, int newSongs)
    {
        var album = await GetByTitleAsync(title);
        if (album == null)
            throw new EntityNotFoundException(nameof(Album));

        if (album.Songs == newSongs)
            throw new SamePropertyNameException(nameof(Album.Songs));
        album.Songs = newSongs;

        _unit.Albums.Update(album, e => e.Songs);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateTitleAsync(string title, [StringLength(50, ErrorMessage = "Maximum length is 50!")] string newTitle)
    {
        var album = await GetByTitleAsync(title);
        if (album == null)
            throw new EntityNotFoundException(nameof(Album));

        if (album.Title == newTitle)
            throw new SamePropertyNameException(nameof(Album.Title));

        var albumByNewTitle = await _unit.Albums.GetByTitleAsync(newTitle);
        if (albumByNewTitle != null)
            throw new EntityAlreadyExistsException(nameof(Album));

        album.Title = newTitle;

        _unit.Albums.Update(album, e => e.Title);
        await _unit.SaveChangesAsync();
    }

    public async Task AddSongAsync(string albumTitle, string songTitle, int? position = null) =>
        await _songAlbumService.UpdateAsync(songTitle, albumTitle, position);

    public async Task AddUserAsync(string albumTitle, string username) =>
        await _userAlbumService.CreateAsync(username, albumTitle);

    public async Task AddArtistAsync(string albumTitle, string artistName) =>
       await _albumArtistService.CreateAsync(albumTitle, artistName);
}
