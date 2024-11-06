using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Common;

public interface IEntityService<T, TDto> : IService<T, TDto> where T : class, IEntity where TDto : class
{
    Task<T?> GetByIdAsync(int id, Expression<Func<T, bool>>? expression = null);
}
