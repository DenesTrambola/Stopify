using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Contracts.Services;
using Stopify.Domain.DTOs;
using Stopify.Domain.Entities;
using Stopify.Exceptions.ValidationExceptions;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Stopify.Domain.Services;

public class CountryService : ICountryService
{
    private readonly IUnitOfWork _unit;
    private readonly IArtistService _artistService;

    public CountryService(IUnitOfWork unit, IArtistService artistService)
    {
        _unit = unit;
        _artistService = artistService;
    }

    public async Task AddArtistAsync(string countryName, string artistName)
    {
        var country = await _unit.Countries.GetByNameAsync(countryName);
        if (country is null)
            throw new EntityNotFoundException(nameof(country));

        var artist = await _unit.Artists.GetByNameAsync(artistName);
        if (artist is null)
            throw new EntityNotFoundException(nameof(artist));

        if (country.Artists.Any(a => a.Id == artist.Id))
            throw new EntityAlreadyExistsException(nameof(artist));

        country.Artists.Add(artist);
        artist.CountryId = country.Id;

        _unit.Artists.Update(artist, e => e.CountryId);
        await _unit.SaveChangesAsync();
    }

    public async Task CreateAsync(Country entity)
    {
        var country = await _unit.Countries.GetByNameAsync(entity.Name);
        if (country != null)
            throw new EntityAlreadyExistsException(nameof(country));

        await _unit.Countries.AddAsync(entity);
        await _unit.SaveChangesAsync();
    }

    public async Task<IEnumerable<Country>?> GetAllAsync(Expression<Func<Country, bool>>? expression = null) =>
        await _unit.Countries.GetAllAsync(expression);

    public async Task<Country?> GetByIdAsync(int id, Expression<Func<Country, bool>>? expression = null) =>
        await _unit.Countries.GetByIdAsync(id, expression);

    public async Task<Country?> GetFirstByNameAsync(string name, Expression<Func<Country, bool>>? expression = null) =>
        await _unit.Countries.GetByNameAsync(name, expression);

    public async Task RemoveAsync(CountryDto dto)
    {
        var country = await _unit.Countries.GetByNameAsync(dto.Name);
        if (country == null)
            throw new EntityNotFoundException(nameof(country));

        var artists = await _unit.Artists.GetAllByCountryIdAsync(country.Id);
        if (artists is not null)
            foreach (var artist in artists)
                await _artistService.UpdateCountryAsync(artist.Name, null);

        _unit.Countries.Remove(country);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateNameAsync(string name, [StringLength(60, ErrorMessage = "Maximum length is 60!")] string newName)
    {
        var country = await GetFirstByNameAsync(name);
        if (country == null)
            throw new EntityNotFoundException(nameof(country));

        if (country.Name == newName)
            throw new SamePropertyNameException(nameof(country) + " " + nameof(country.Name));

        var countryByNewName = await _unit.Countries.GetByNameAsync(newName);
        if (countryByNewName != null)
            throw new EntityAlreadyExistsException(nameof(country));

        country.Name = newName;

        _unit.Countries.Update(country, e => e.Name);
        await _unit.SaveChangesAsync();
    }
}
