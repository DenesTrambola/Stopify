using Microsoft.EntityFrameworkCore;
using Stopify.Domain.Contracts.Common;
using System.Linq.Expressions;

namespace Stopify.Infrastructure.Persistence.Repositories;

public abstract class GenericEntityRepository<T> : GenericRepository<T>, IEntityRepository<T> where T : class, IEntity
{
    private readonly DbContext _context;

    public GenericEntityRepository(DbContext context) : base(context) =>
        _context = context;

    public async Task<T?> GetByIdAsync(int id, Expression<Func<T, bool>>? expression = null)
    {
        IQueryable<T> query = _dbSet.AsNoTracking();

        var navigationProperties = _context.Model
            .FindEntityType(typeof(T))?
            .GetNavigations()
            .Select(n => n.Name);

        if (navigationProperties != null)
            foreach (var navProperty in navigationProperties)
                query = query.Include(navProperty);

        if (expression != null)
            query = query.Where(expression);

        return await query.FirstOrDefaultAsync(e => e.Id == id);
    }
}
