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

    public async Task UpdateAsync(string songTitle, string? albumTitle, int? position)
    {
        // Search for the song by 'songTitle'
        var song = await _unit.Songs.GetByTitleAsync(songTitle);
        if (song == null)
            throw new EntityNotFoundException(nameof(Song));

        // removes song from it's album (if it has one)
        if (song.AlbumId is not null && song.Album is not null)
        {
            var songsByAlbum = await _unit.Songs.GetAllByAlbumIdAsync(song.AlbumId.Value);
            if (songsByAlbum == null)
                throw new EntityNotFoundException(nameof(Song));

            foreach (var s in songsByAlbum.Where(s => s.AlbumPosition > song.AlbumPosition))
            {
                s.AlbumPosition--;
                _unit.Songs.Update(s, e => e.AlbumPosition);
            }

            song.Album.Songs = songsByAlbum.Count();
            song.Album.Songs = songsByAlbum.Count() - 1;
            song.Album.Duration -= song.Duration;

            _unit.Albums.Update(song.Album, e => e.Songs, e => e.Duration);
        }

        // remove song's 'Cover', 'AlbumId' and 'AlbumPosition' if parameter 'albumTitle' is not setted
        if (albumTitle is null)
        {
            song.Cover = $"{Song.MainCoverPath}song-cover-default.jpg";
            song.AlbumId = null;
            song.AlbumPosition = null;
        }
        // setup a connection between song and album
        else
        {
            var album = await _unit.Albums.GetByTitleAsync(albumTitle);
            if (album is null)
                throw new EntityNotFoundException(nameof(Album));

            if (song.AlbumId == album.Id && song.AlbumPosition == (position ?? song.AlbumPosition))
                throw new SamePropertyNameException(nameof(Song) + " " + nameof(Song.AlbumId));

            album.Duration += song.Duration;
            album.Songs = album.SongsNavigation.Count() + 1;

            if (position is not null)
            {
                foreach (var s in album.SongsNavigation.Where(s => s.AlbumPosition >= position))
                {
                    s.AlbumPosition++;
                    _unit.Songs.Update(s, s => s.AlbumPosition);
                }
                song.AlbumPosition = position;
            }
            else
                song.AlbumPosition = album.Songs;

            _unit.Albums.Update(album, e => e.Songs, e => e.Duration);

            song.Cover = album.Cover;
            song.AlbumId = album.Id;
        }

        // save changes
        _unit.Songs.Update(song, e => e.AlbumId, e => e.Cover, e => e.AlbumPosition);
        await _unit.SaveChangesAsync();
    }
}
