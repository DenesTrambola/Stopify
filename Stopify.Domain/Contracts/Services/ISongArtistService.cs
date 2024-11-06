namespace Stopify.Domain.Contracts.Services;

public interface ISongArtistService
{
    Task CreateAsync(string songTitle, string artistName);
}
