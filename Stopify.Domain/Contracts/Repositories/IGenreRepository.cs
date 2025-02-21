using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Repositories;

public interface IGenreRepository : IEntityRepository<Genre>
{
    Task<Genre?> GetByNameAsync(string name, Expression<Func<Genre, bool>>? expression = null);
}
