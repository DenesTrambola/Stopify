using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Other;
using Stopify.Exceptions.ValidationExceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stopify.Domain.Entities;

public partial class Song : IEntity
{
    public Song(string title, int duration, DateOnly releaseDate, string path, string? cover = null)
    {
        Title = title;
        Duration = duration;
        ReleaseDate = releaseDate;
        Path = MainSongPath + path + ".mp3";
        Cover = cover is null ? $"{MainCoverPath}song-cover-default.jpg" :
            UrlValidation.CheckFormat(cover) ? cover : $"{MainCoverPath}{cover}.jpg";

        if (!UrlValidation.CheckFormat(Path))
            throw new InvalidUrlException();
        if (!UrlValidation.CheckFormat(Cover))
            throw new InvalidUrlException();
    }

    [NotMapped]
    public static string MainSongPath { get; } = "https://blobstopify.blob.core.windows.net/songs/";

    [NotMapped]
    public static string MainCoverPath { get; } = "https://blobstopify.blob.core.windows.net/song-covers/";

    [Required(ErrorMessage = "Id error!")]
    public int Id { get; set; }

    [StringLength(200, ErrorMessage = "Maximum length is 200!")]
    public string Title { get; set; } = null!;

    [ForeignKey(nameof(Entities.Album))]
    public int? AlbumId { get; set; }

    public int? AlbumPosition { get; set; }

    public int Duration { get; set; }

    public DateOnly ReleaseDate { get; set; }

    [StringLength(2048, ErrorMessage = "Maximum length is 2048!")]
    public string Path { get; set; } = null!;

    [StringLength(2048, ErrorMessage = "Maximum length is 2048!")]
    public string Cover { get; set; }

    public virtual Album? Album { get; set; }

    public virtual ICollection<SongQueue> Queues { get; set; } = new List<SongQueue>();

    public virtual ICollection<RecentPlayed> RecentPlays { get; set; } = new List<RecentPlayed>();

    public virtual ICollection<SongPlaylist> SongPlaylists { get; set; } = new List<SongPlaylist>();

    public virtual ICollection<PlaybackHistory> PlaybackHistories { get; set; } = new List<PlaybackHistory>();

    public virtual ICollection<Artist> Artists { get; set; } = new List<Artist>();

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
