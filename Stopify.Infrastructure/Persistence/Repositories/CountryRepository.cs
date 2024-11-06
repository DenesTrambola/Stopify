using Microsoft.EntityFrameworkCore;
using Stopify.Domain.Contracts.Repositories;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Infrastructure.Persistence.Repositories;

public class CountryRepository : GenericEntityRepository<Country>, ICountryRepository
{
    private readonly StopifyDbContext _context;

    public CountryRepository(StopifyDbContext context) : base(context) =>
        _context = context;

    public async Task<Country?> GetByNameAsync(string name, Expression<Func<Country, bool>>? expression = null) =>
        await _context.Countries.Include(e => e.Artists).Where(e => e.Name == name).Where(expression ?? (_ => true)).FirstOrDefaultAsync();
}
