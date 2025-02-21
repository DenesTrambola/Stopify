using Microsoft.EntityFrameworkCore;
using Stopify.Domain.Contracts.Repositories;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Infrastructure.Persistence.Repositories;

public class AlbumRepository : GenericEntityRepository<Album>, IAlbumRepository
{
    private readonly StopifyDbContext _context;

    public AlbumRepository(StopifyDbContext context) : base(context) =>
        _context = context;

    public async Task<IEnumerable<Album>?> GetAllByCoverAsync(string cover, Expression<Func<Album, bool>>? expression = null) =>
        await _context.Albums.Where(e => e.Cover == cover)
        .Include(e => e.SongsNavigation)
        .Include(e => e.UserAlbums)
        .Include(e => e.Artists)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<Album>?> GetAllByDurationAsync(int duration, Expression<Func<Album, bool>>? expression = null) =>
        await _context.Albums.Where(e => e.Duration == duration)
        .Include(e => e.SongsNavigation)
        .Include(e => e.UserAlbums)
        .Include(e => e.Artists)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<Album>?> GetAllByReleaseDateAsync(DateOnly releaseDate, Expression<Func<Album, bool>>? expression = null) =>
        await _context.Albums.Where(e => e.ReleaseDate == releaseDate)
        .Include(e => e.SongsNavigation)
        .Include(e => e.UserAlbums)
        .Include(e => e.Artists)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<Album>?> GetAllBySavesAsync(int saves, Expression<Func<Album, bool>>? expression = null) =>
        await _context.Albums.Where(e => e.Saves == saves)
        .Include(e => e.SongsNavigation)
        .Include(e => e.UserAlbums)
        .Include(e => e.Artists)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<IEnumerable<Album>?> GetAllBySongsAsync(int songs, Expression<Func<Album, bool>>? expression = null) =>
        await _context.Albums.Where(e => e.Songs == songs)
        .Include(e => e.SongsNavigation)
        .Include(e => e.UserAlbums)
        .Include(e => e.Artists)
        .Where(expression ?? (_ => true)).ToListAsync();

    public async Task<Album?> GetFirstByCoverAsync(string cover, Expression<Func<Album, bool>>? expression = null) =>
        await _context.Albums.Where(e => e.Cover == cover)
        .Include(e => e.SongsNavigation)
        .Include(e => e.UserAlbums)
        .Include(e => e.Artists)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<Album?> GetFirstByDurationAsync(int duration, Expression<Func<Album, bool>>? expression = null) =>
        await _context.Albums.Where(e => e.Duration == duration)
        .Include(e => e.SongsNavigation)
        .Include(e => e.UserAlbums)
        .Include(e => e.Artists)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<Album?> GetFirstByReleaseDateAsync(DateOnly releaseDate, Expression<Func<Album, bool>>? expression = null) =>
        await _context.Albums.Where(e => e.ReleaseDate == releaseDate)
        .Include(e => e.SongsNavigation)
        .Include(e => e.UserAlbums)
        .Include(e => e.Artists)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<Album?> GetFirstBySavesAsync(int saves, Expression<Func<Album, bool>>? expression = null) =>
        await _context.Albums.Where(e => e.Saves == saves)
        .Include(e => e.SongsNavigation)
        .Include(e => e.UserAlbums)
        .Include(e => e.Artists)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<Album?> GetFirstBySongsAsync(int songs, Expression<Func<Album, bool>>? expression = null) =>
        await _context.Albums.Where(e => e.Songs == songs)
        .Include(e => e.SongsNavigation)
        .Include(e => e.UserAlbums)
        .Include(e => e.Artists)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();

    public async Task<Album?> GetByTitleAsync(string title, Expression<Func<Album, bool>>? expression = null) =>
        await _context.Albums.Where(e => e.Title == title)
        .Include(e => e.SongsNavigation)
        .Include(e => e.UserAlbums)
        .Include(e => e.Artists)
        .Where(expression ?? (_ => true)).FirstOrDefaultAsync();
}
