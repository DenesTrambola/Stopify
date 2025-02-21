using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Contracts.Services;
using Stopify.Domain.Entities;
using Stopify.Exceptions.ValidationExceptions;

namespace Stopify.Domain.Services;

public class AlbumArtistService : IAlbumArtistService
{
    private readonly IUnitOfWork _unit;

    public AlbumArtistService(IUnitOfWork unit) =>
        _unit = unit;

    public async Task CreateAsync(string albumTitle, string artistName)
    {
        var album = await _unit.Albums.GetByTitleAsync(albumTitle);
        if (album is null)
            throw new EntityNotFoundException(nameof(Album));

        var artist = await _unit.Artists.GetByNameAsync(artistName);
        if (artist is null)
            throw new EntityNotFoundException(nameof(Artist));

        if (album.Artists.Any(a => a.Id == artist.Id) || artist.Albums.Any(a => a.Id == album.Id))
            throw new EntityAlreadyExistsException($"Relationship {nameof(Album)}-{nameof(Artist)}");

        album.Artists.Add(artist);
        artist.Albums.Add(album);

        await _unit.SaveChangesAsync();
    }
}
