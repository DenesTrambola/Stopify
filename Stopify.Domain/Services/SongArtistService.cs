﻿using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Contracts.Services;
using Stopify.Exceptions.ValidationExceptions;

namespace Stopify.Domain.Services;

public class SongArtistService : ISongArtistService
{
    private readonly IUnitOfWork _unit;

    public SongArtistService(IUnitOfWork unit) =>
        _unit = unit;

    public async Task CreateAsync(string songTitle, string artistName)
    {
        var song = await _unit.Songs.GetByTitleAsync(songTitle);
        if (song is null)
            throw new EntityNotFoundException(nameof(song));

        var artist = await _unit.Artists.GetByNameAsync(artistName);
        if (artist is null)
            throw new EntityNotFoundException(nameof(artist));

        if (song.Artists.Any(a => a.Id == artist.Id) || artist.Songs.Any(s => s.Id == song.Id))
            throw new EntityAlreadyExistsException($"Relationship {nameof(song)}-{nameof(artist)}");

        song.Artists.Add(artist);
        artist.Songs.Add(song);

        await _unit.SaveChangesAsync();
    }
}