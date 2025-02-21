using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Repositories;

public interface IPlaylistRepository : IEntityRepository<Playlist>
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
}
