using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Repositories;

public interface IArtistRepository : IEntityRepository<Artist>
{
    Task<Artist?> GetByNameAsync(string name, Expression<Func<Artist, bool>>? expression = null);
    Task<Artist?> GetFirstByBioAsync(string bio, Expression<Func<Artist, bool>>? expression = null);
    Task<Artist?> GetFirstByCountryIdAsync(int countryId, Expression<Func<Artist, bool>>? expression = null);
    Task<Artist?> GetFirstByAvatarAsync(string avatar, Expression<Func<Artist, bool>>? expression = null);
    Task<Artist?> GetFirstByBgImageAsync(string bgImage, Expression<Func<Artist, bool>>? expression = null);
    Task<Artist?> GetFirstVerifiedAsync(Expression<Func<Artist, bool>>? expression = null);
    Task<Artist?> GetFirstNotVerifiedAsync(Expression<Func<Artist, bool>>? expression = null);

    Task<IEnumerable<Artist>?> GetAllByBioAsync(string bio, Expression<Func<Artist, bool>>? expression = null);
    Task<IEnumerable<Artist>?> GetAllByCountryIdAsync(int countryId, Expression<Func<Artist, bool>>? expression = null);
    Task<IEnumerable<Artist>?> GetAllByAvatarAsync(string avatar, Expression<Func<Artist, bool>>? expression = null);
    Task<IEnumerable<Artist>?> GetAllByBgImageAsync(string bgImage, Expression<Func<Artist, bool>>? expression = null);
    Task<IEnumerable<Artist>?> GetAllVerifiedAsync(Expression<Func<Artist, bool>>? expression = null);
    Task<IEnumerable<Artist>?> GetAllNotVerifiedAsync(Expression<Func<Artist, bool>>? expression = null);
}
