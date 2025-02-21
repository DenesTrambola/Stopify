using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Common;

public interface IEntityRepository<T> : IRepository<T> where T : class, IEntity
{
    Task<T?> GetByIdAsync(int id, Expression<Func<T, bool>>? expression = null);
}
