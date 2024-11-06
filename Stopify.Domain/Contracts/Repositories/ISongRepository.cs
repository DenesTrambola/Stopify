using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Repositories;

public interface ISongRepository : IEntityRepository<Song>
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
}
