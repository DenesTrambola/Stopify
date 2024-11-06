using Microsoft.EntityFrameworkCore;
using Stopify.Domain.Contracts.Repositories;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Infrastructure.Persistence.Repositories;

public class ArtistRepository : GenericEntityRepository<Artist>, IArtistRepository
{
    private readonly StopifyDbContext _context;

    public ArtistRepository(StopifyDbContext context) : base(context) =>
        _context = context;

    public async Task<IEnumerable<Artist>?> GetAllByAvatarAsync(string avatar, Expression<Func<Artist, bool>>? expression = null) =>
        await _context.Artists.Where(e => e.Avatar == avatar)
        .Include(e => e.Country)
        .Include(e => e.UserArtists)
        .Include(e => e.Albums)
        .Include(e => e.Songs)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<Artist>?> GetAllByBgImageAsync(string bgImage, Expression<Func<Artist, bool>>? expression = null) =>
        await _context.Artists.Where(e => e.BgImage == bgImage)
        .Include(e => e.Country)
        .Include(e => e.UserArtists)
        .Include(e => e.Albums)
        .Include(e => e.Songs)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<Artist>?> GetAllByBioAsync(string bio, Expression<Func<Artist, bool>>? expression = null) =>
        await _context.Artists.Where(e => e.Bio == bio)
        .Include(e => e.Country)
        .Include(e => e.UserArtists)
        .Include(e => e.Albums)
        .Include(e => e.Songs)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<Artist>?> GetAllByCountryIdAsync(int countryId, Expression<Func<Artist, bool>>? expression = null) =>
        await _context.Artists.Where(e => e.CountryId == countryId)
        .Include(e => e.Country)
        .Include(e => e.UserArtists)
        .Include(e => e.Albums)
        .Include(e => e.Songs)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<Artist>?> GetAllNotVerifiedAsync(Expression<Func<Artist, bool>>? expression = null) =>
        await _context.Artists.Where(e => !e.IsVerified)
        .Include(e => e.Country)
        .Include(e => e.UserArtists)
        .Include(e => e.Albums)
        .Include(e => e.Songs)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<Artist>?> GetAllVerifiedAsync(Expression<Func<Artist, bool>>? expression = null) =>
        await _context.Artists.Where(e => e.IsVerified)
        .Include(e => e.Country)
        .Include(e => e.UserArtists)
        .Include(e => e.Albums)
        .Include(e => e.Songs)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<Artist?> GetFirstByAvatarAsync(string avatar, Expression<Func<Artist, bool>>? expression = null) =>
        await _context.Artists.Where(e => e.Avatar == avatar)
        .Include(e => e.Country)
        .Include(e => e.UserArtists)
        .Include(e => e.Albums)
        .Include(e => e.Songs)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<Artist?> GetFirstByBgImageAsync(string bgImage, Expression<Func<Artist, bool>>? expression = null) =>
        await _context.Artists.Where(e => e.BgImage == bgImage)
        .Include(e => e.Country)
        .Include(e => e.UserArtists)
        .Include(e => e.Albums)
        .Include(e => e.Songs)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<Artist?> GetFirstByBioAsync(string bio, Expression<Func<Artist, bool>>? expression = null) =>
        await _context.Artists.Where(e => e.Bio == bio)
        .Include(e => e.Country)
        .Include(e => e.UserArtists)
        .Include(e => e.Albums)
        .Include(e => e.Songs)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<Artist?> GetFirstByCountryIdAsync(int countryId, Expression<Func<Artist, bool>>? expression = null) =>
        await _context.Artists.Where(e => e.CountryId == countryId)
        .Include(e => e.Country)
        .Include(e => e.UserArtists)
        .Include(e => e.Albums)
        .Include(e => e.Songs)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<Artist?> GetByNameAsync(string name, Expression<Func<Artist, bool>>? expression = null) =>
        await _context.Artists.Where(e => e.Name == name)
        .Include(e => e.Country)
        .Include(e => e.UserArtists)
        .Include(e => e.Albums)
        .Include(e => e.Songs)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<Artist?> GetFirstNotVerifiedAsync(Expression<Func<Artist, bool>>? expression = null) =>
        await _context.Artists.Where(e => !e.IsVerified)
        .Include(e => e.Country)
        .Include(e => e.UserArtists)
        .Include(e => e.Albums)
        .Include(e => e.Songs)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<Artist?> GetFirstVerifiedAsync(Expression<Func<Artist, bool>>? expression = null) =>
        await _context.Artists.Where(e => e.IsVerified)
        .Include(e => e.Country)
        .Include(e => e.UserArtists)
        .Include(e => e.Albums)
        .Include(e => e.Songs)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();
}
