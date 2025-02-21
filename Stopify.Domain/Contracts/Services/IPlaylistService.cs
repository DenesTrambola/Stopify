using Stopify.Domain.Contracts.Common;
using Stopify.Domain.DTOs;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Services;

public interface IPlaylistService : IEntityService<Playlist, PlaylistDto>
{
    Task<Playlist?> GetByTitleAsync(string title, Expression<Func<Playlist, bool>>? expression = null);
    Task<Playlist?> GetFirstByDescriptionAsync(string description, Expression<Func<Playlist, bool>>? expression = null);
    Task<Playlist?> GetFirstPublicAsync(Expression<Func<Playlist, bool>>? expression = null);
    Task<Playlist?> GetFirstNotPublicAsync(Expression<Func<Playlist, bool>>? expression = null);
    Task<Playlist?> GetFirstBySavesAsync(int saves, Expression<Func<Playlist, bool>>? expression = null);
    Task<Playlist?> GetFirstBySongsAsync(int songs, Expression<Func<Playlist, bool>>? expression = null);
    Task<Playlist?> GetFirstByDurationAsync(int duration, Expression<Func<Playlist, bool>>? expression = null);
    Task<Playlist?> GetFirstByCoverAsync(string cover, Expression<Func<Playlist, bool>>? expression = null);

    Task<IEnumerable<Playlist>?> GetAllByDescriptionAsync(string description, Expression<Func<Playlist, bool>>? expression = null);
    Task<IEnumerable<Playlist>?> GetAllPublicAsync(Expression<Func<Playlist, bool>>? expression = null);
    Task<IEnumerable<Playlist>?> GetAllNotPublicAsync(Expression<Func<Playlist, bool>>? expression = null);
    Task<IEnumerable<Playlist>?> GetAllBySavesAsync(int saves, Expression<Func<Playlist, bool>>? expression = null);
    Task<IEnumerable<Playlist>?> GetAllBySongsAsync(int songs, Expression<Func<Playlist, bool>>? expression = null);
    Task<IEnumerable<Playlist>?> GetAllByDurationAsync(int duration, Expression<Func<Playlist, bool>>? expression = null);
    Task<IEnumerable<Playlist>?> GetAllByCoverAsync(string cover, Expression<Func<Playlist, bool>>? expression = null);

    Task UpdateTitleAsync(string title, string newTitle);
    Task UpdateDescriptionAsync(string title, string newDescription);
    Task UpdatePublicAsync(string title, bool isPublic);
    Task UpdateSavesAsync(string title, int newSaves);
    Task UpdateSongPositionAsync(string title, string songTitle, int position);
    Task UpdateSongsAsync(string title, int newSongs);
    Task UpdateDurationAsync(string title, int newDuration);
    Task UpdateCoverAsync(string title, string? newCover);

    Task AddSongAsync(string playlistTitle, string songTitle, int? position = null);
    Task AddUserAsync(string playlistTitle, string username);
}
