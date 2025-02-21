using Microsoft.EntityFrameworkCore;
using Stopify.Domain.Contracts.Common;
using System.Linq.Expressions;

namespace Stopify.Infrastructure.Persistence.Repositories;

public abstract class GenericRepository<T> : IRepository<T> where T : class
{
    private readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task AddAsync(T entity) =>
        await _dbSet.AddAsync(entity);

    public async Task<IEnumerable<T>?> GetAllAsync(Expression<Func<T, bool>>? expression = null)
    {
        IQueryable<T> query = _dbSet;

        var navigationProperties = _context.Model
         .FindEntityType(typeof(T))?
         .GetNavigations()
         .Select(n => n.Name);

        if (navigationProperties != null)
            foreach (var navProperty in navigationProperties)
                query = query.Include(navProperty);

        if (expression != null)
            query = query.Where(expression);

        return await query.ToListAsync();
    }

    public void Remove(T entity) =>
        _context.Set<T>().Remove(entity);

    public void Update(T entity, params Expression<Func<T, object>>[] updatedProperties)
    {
        _dbSet.Attach(entity);
        foreach (var property in updatedProperties)
            _context.Entry(entity).Property(property).IsModified = true;
    }
}
