namespace Stopify.Domain.Contracts.Services;

public interface IAlbumArtistService
{
    Task CreateAsync(string albumTitle, string artistName);
}
