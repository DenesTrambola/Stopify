using Stopify.Domain.Contracts.Common;
using Stopify.Domain.DTOs;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Services;

public interface IGenreService : IEntityService<Genre, GenreDto>
{
    Task<Genre?> GetFirstByNameAsync(string name, Expression<Func<Genre, bool>>? expression = null);
    Task UpdateNameAsync(string name, string newName);

    Task AddSongAsync(string genreName, string songTitle);
}
