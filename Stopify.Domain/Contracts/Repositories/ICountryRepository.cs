using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Repositories;

public interface ICountryRepository : IEntityRepository<Country>
{
    Task<Country?> GetByNameAsync(string name, Expression<Func<Country, bool>>? expression = null);
}
