using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Common;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>?> GetAllAsync(Expression<Func<T, bool>>? expression = null);
    Task AddAsync(T entity);
    void Update(T entity, params Expression<Func<T, object>>[] updatedProperties);
    void Remove(T entity);
}
