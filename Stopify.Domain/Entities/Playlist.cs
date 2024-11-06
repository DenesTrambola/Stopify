using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Other;
using Stopify.Exceptions.ValidationExceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stopify.Domain.Entities;

public partial class Playlist : IEntity
{
    public Playlist(string title, bool isPublic, string? description = null, string? cover = null)
    {
        Title = title;
        IsPublic = isPublic;
        Description = description;
        Cover = cover is null ? $"{MainCoverPath}playlist-cover-default.jpg" :
            UrlValidation.CheckFormat(cover) ? cover : $"{MainCoverPath}{cover}.jpg";

        if (!UrlValidation.CheckFormat(Cover))
            throw new InvalidUrlException();
    }

    [NotMapped]
    public static string MainCoverPath { get; } = "https://blobstopify.blob.core.windows.net/playlist-covers/";

    [Required(ErrorMessage = "Id error!")]
    public int Id { get; set; }

    [StringLength(50, ErrorMessage = "Maximum length is 50!")]
    public string Title { get; set; } = null!;

    [StringLength(200, ErrorMessage = "Maximum length is 200!")]
    public string? Description { get; set; }

    public bool IsPublic { get; set; }

    public int Saves { get; set; }

    public int Songs { get; set; }

    [StringLength(2048)]
    public string Cover { get; set; }

    public int Duration { get; set; }

    public virtual ICollection<SongPlaylist> SongPlaylists { get; set; } = new List<SongPlaylist>();

    public virtual ICollection<UserPlaylist> UserPlaylists { get; set; } = new List<UserPlaylist>();
}
