using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Contracts.Services;
using Stopify.Domain.Entities;
using Stopify.Exceptions.ValidationExceptions;

namespace Stopify.Domain.Services;

public class SongAlbumService : ISongAlbumService
{
    private readonly IUnitOfWork _unit;

    public SongAlbumService(IUnitOfWork unit) =>
        _unit = unit;

    // the dynamic position updating needs to be implemented in the future
    public async Task UpdateAsync(string songTitle, string? albumTitle, int? position = null)
    {
        var song = await _unit.Songs.GetByTitleAsync(songTitle);
        if (song == null)
            throw new EntityNotFoundException(nameof(song));

        if (song.AlbumId is not null && song.Album is not null)
        {
            IEnumerable<Song>? songsByAlbumId = await _unit.Songs.GetAllByAlbumIdAsync((int)song.AlbumId);
            song.Album.Songs = songsByAlbumId is null ? 0 : songsByAlbumId.Count();
            song.Album.Songs--;
            song.Album.Duration -= song.Duration;

            _unit.Albums.Update(song.Album, e => e.Songs, e => e.Duration);
        }

        if (albumTitle is null)
        {
            song.AlbumId = null;
            song.Cover = $"{Song.MainCoverPath}song-cover-default.jpg";
        }
        else
        {
            var album = await _unit.Albums.GetByTitleAsync(albumTitle);
            if (album == null)
                throw new EntityNotFoundException(nameof(album));

            if (song.AlbumId == album.Id)
                throw new SamePropertyNameException(nameof(song) + " " + nameof(song.AlbumId));

            album.Duration += song.Duration;
            album.Songs++;
            _unit.Albums.Update(album, e => e.Songs, e => e.Duration);

            song.Cover = album.Cover;
            song.AlbumId = album.Id;
        }

        _unit.Songs.Update(song, e => e.AlbumId, e => e.Cover);
        await _unit.SaveChangesAsync();
    }
}
