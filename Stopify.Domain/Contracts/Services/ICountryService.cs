using Stopify.Domain.Contracts.Common;
using Stopify.Domain.DTOs;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Services;

public interface ICountryService : IEntityService<Country, CountryDto>
{
    Task<Country?> GetFirstByNameAsync(string name, Expression<Func<Country, bool>>? expression = null);
    Task UpdateNameAsync(string name, string newName);

    Task AddArtistAsync(string countryName, string artistName);
}
