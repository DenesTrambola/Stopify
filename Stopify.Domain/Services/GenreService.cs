using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Contracts.Services;
using Stopify.Domain.DTOs;
using Stopify.Domain.Entities;
using Stopify.Exceptions.ValidationExceptions;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Stopify.Domain.Services;

public class GenreService : IGenreService
{
    private readonly IUnitOfWork _unit;
    private readonly ISongGenreService _songGenreService;

    public GenreService(IUnitOfWork unit, ISongGenreService songGenreService)
    {
        _unit = unit;
        _songGenreService = songGenreService;
    }

    public async Task AddSongAsync(string genreName, string songTitle) =>
       await _songGenreService.CreateAsync(songTitle, genreName);

    public async Task CreateAsync(Genre entity)
    {
        var genre = await _unit.Genres.GetByNameAsync(entity.Name);
        if (genre != null)
            throw new EntityAlreadyExistsException(nameof(Genre));

        await _unit.Genres.AddAsync(entity);
        await _unit.SaveChangesAsync();
    }

    public async Task<IEnumerable<Genre>?> GetAllAsync(Expression<Func<Genre, bool>>? expression = null) =>
        await _unit.Genres.GetAllAsync(expression);

    public async Task<Genre?> GetByIdAsync(int id, Expression<Func<Genre, bool>>? expression = null) =>
        await _unit.Genres.GetByIdAsync(id, expression);

    public async Task<Genre?> GetFirstByNameAsync(string name, Expression<Func<Genre, bool>>? expression = null) =>
        await _unit.Genres.GetByNameAsync(name, expression);

    public async Task RemoveAsync(GenreDto dto)
    {
        var genre = await _unit.Genres.GetByNameAsync(dto.Name);
        if (genre == null)
            throw new EntityNotFoundException(nameof(Genre));

        _unit.Genres.Remove(genre);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateNameAsync(string name, [StringLength(50, ErrorMessage = "Maximum length is 50!")] string newName)
    {
        var genre = await GetFirstByNameAsync(name);
        if (genre == null)
            throw new EntityNotFoundException(nameof(Genre));

        if (genre.Name == newName)
            throw new SamePropertyNameException(nameof(Genre) + " " + nameof(Genre.Name));

        var genreByNewName = await _unit.Genres.GetByNameAsync(newName);
        if (genreByNewName != null)
            throw new EntityAlreadyExistsException(nameof(Genre));

        genre.Name = newName;

        _unit.Genres.Update(genre, e => e.Name);
        await _unit.SaveChangesAsync();
    }
}
