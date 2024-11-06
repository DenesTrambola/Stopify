using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Common;

public interface IService<T, TDto> where T : class where TDto : class
{
    Task<IEnumerable<T>?> GetAllAsync(Expression<Func<T, bool>>? expression = null);
    Task CreateAsync(T entity);
    Task RemoveAsync(TDto dto);
}
