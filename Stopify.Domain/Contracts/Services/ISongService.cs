using Stopify.Domain.Contracts.Common;
using Stopify.Domain.DTOs;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Services;

public interface ISongService : IEntityService<Song, SongDto>
{
    Task<Song?> GetByTitleAsync(string title, Expression<Func<Song, bool>>? expression = null);
    Task<Song?> GetFirstByAlbumIdAsync(int albumId, Expression<Func<Song, bool>>? expression = null);
    Task<Song?> GetFirstByDurationAsync(int duration, Expression<Func<Song, bool>>? expression = null);
    Task<Song?> GetFirstByReleaseDateAsync(DateOnly releaseDate, Expression<Func<Song, bool>>? expression = null);
    Task<Song?> GetFirstByPathAsync(string path, Expression<Func<Song, bool>>? expression = null);
    Task<Song?> GetFirstByCoverAsync(string cover, Expression<Func<Song, bool>>? expression = null);

    Task<IEnumerable<Song>?> GetAllByAlbumIdAsync(int albumId, Expression<Func<Song, bool>>? expression = null);
    Task<IEnumerable<Song>?> GetAllByDurationAsync(int duration, Expression<Func<Song, bool>>? expression = null);
    Task<IEnumerable<Song>?> GetAllByReleaseDateAsync(DateOnly releaseDate, Expression<Func<Song, bool>>? expression = null);
    Task<IEnumerable<Song>?> GetAllByPathAsync(string path, Expression<Func<Song, bool>>? expression = null);
    Task<IEnumerable<Song>?> GetAllByCoverAsync(string cover, Expression<Func<Song, bool>>? expression = null);

    Task UpdateTitleAsync(string title, string newTitle);
    Task UpdateAlbumAsync(string title, string? newAlbumTitle, int? position = null);
    Task UpdateDurationAsync(string title, int newDuration);
    Task UpdateReleaseDateAsync(string title, DateOnly newReleaseDate);
    Task UpdatePathAsync(string title, string newPath);
    Task UpdateCoverAsync(string title, string? newCover);

    Task AddQueueAsync(string songTitle, string username, int? position = null);
    Task AddRecentPlayedAsync(string songTitle, string username, int? position = null);
    Task AddPlaylistAsync(string songTitle, string playlistTitle, int? position = null);
    Task AddPlaybackHistoryAsync(string songTitle, string username, int? position = null);
    Task AddArtistAsync(string songTitle, string artistName);
    Task AddGenreAsync(string songTitle, string genreName);
}
