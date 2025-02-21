using Stopify.Domain.Contracts.Common;
using Stopify.Domain.DTOs;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Services;


public interface IAlbumService : IEntityService<Album, AlbumDto>
{
    Task<Album?> GetByTitleAsync(string title, Expression<Func<Album, bool>>? expression = null);
    Task<Album?> GetFirstByReleaseDateAsync(DateOnly releaseDate, Expression<Func<Album, bool>>? expression = null);
    Task<Album?> GetFirstByCoverAsync(string cover, Expression<Func<Album, bool>>? expression = null);
    Task<Album?> GetFirstBySavesAsync(int saves, Expression<Func<Album, bool>>? expression = null);
    Task<Album?> GetFirstBySongsAsync(int songs, Expression<Func<Album, bool>>? expression = null);
    Task<Album?> GetFirstByDurationAsync(int duration, Expression<Func<Album, bool>>? expression = null);

    Task<IEnumerable<Album>?> GetAllByReleaseDateAsync(DateOnly releaseDate, Expression<Func<Album, bool>>? expression = null);
    Task<IEnumerable<Album>?> GetAllByCoverAsync(string cover, Expression<Func<Album, bool>>? expression = null);
    Task<IEnumerable<Album>?> GetAllBySavesAsync(int saves, Expression<Func<Album, bool>>? expression = null);
    Task<IEnumerable<Album>?> GetAllBySongsAsync(int songs, Expression<Func<Album, bool>>? expression = null);
    Task<IEnumerable<Album>?> GetAllByDurationAsync(int duration, Expression<Func<Album, bool>>? expression = null);

    Task UpdateTitleAsync(string title, string newTitle);
    Task UpdateReleaseDateAsync(string title, DateOnly newReleaseDate);
    Task UpdateCoverAsync(string title, string newCover);
    Task UpdateSavesAsync(string title, int newSaves);
    Task UpdateSongsAsync(string title, int newSongs);
    Task UpdateDurationAsync(string title, int newDuration);

    Task AddSongAsync(string albumTitle, string songTitle, int? position = null);
    Task AddUserAsync(string albumTitle, string username);
    Task AddArtistAsync(string albumTitle, string artistName);
}
