﻿using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Contracts.Services;
using Stopify.Exceptions.ValidationExceptions;

namespace Stopify.Domain.Services;

public class SongGenreService : ISongGenreService
{
    private readonly IUnitOfWork _unit;

    public SongGenreService(IUnitOfWork unit) =>
        _unit = unit;

    public async Task CreateAsync(string songTitle, string genreName)
    {
        var song = await _unit.Songs.GetByTitleAsync(songTitle);
        if (song is null)
            throw new EntityNotFoundException(nameof(song));

        var genre = await _unit.Genres.GetByNameAsync(genreName);
        if (genre is null)
            throw new EntityNotFoundException(nameof(genre));

        if (song.Genres.Any(g => g.Id == genre.Id) || genre.Songs.Any(s => s.Id == song.Id))
            throw new EntityAlreadyExistsException($"Relationship {nameof(song)}-{nameof(genre)}");

        song.Genres.Add(genre);
        genre.Songs.Add(song);

        await _unit.SaveChangesAsync();
    }
}