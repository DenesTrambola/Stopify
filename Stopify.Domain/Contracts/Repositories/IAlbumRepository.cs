using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Repositories;

public interface IAlbumRepository : IEntityRepository<Album>
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
}
