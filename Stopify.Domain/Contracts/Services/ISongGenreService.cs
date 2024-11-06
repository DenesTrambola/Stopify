namespace Stopify.Domain.Contracts.Services;

public interface ISongGenreService
{
    Task CreateAsync(string songTitle, string genreName);
}
