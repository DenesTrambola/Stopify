using Microsoft.EntityFrameworkCore;
using Stopify.Domain.Contracts.Repositories;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Infrastructure.Persistence.Repositories;

public class GenreRepository : GenericEntityRepository<Genre>, IGenreRepository
{
    private readonly StopifyDbContext _context;

    public GenreRepository(StopifyDbContext context) : base(context) =>
        _context = context;

    public async Task<Genre?> GetByNameAsync(string name, Expression<Func<Genre, bool>>? expression = null) =>
        await _context.Genres.Where(e => e.Name == name).Include(e => e.Songs).Where(expression ?? (_ => true)).FirstOrDefaultAsync();
}
