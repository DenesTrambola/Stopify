using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Other;
using Stopify.Exceptions.ValidationExceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stopify.Domain.Entities;

public partial class Album : IEntity
{
    public Album(string title, DateOnly releaseDate, string cover)
    {
        Title = title;
        ReleaseDate = releaseDate;
        Cover = UrlValidation.CheckFormat(cover) ? cover : $"{MainCoverPath}{cover}.jpg";

        if (!UrlValidation.CheckFormat(Cover))
            throw new InvalidUrlException();
    }

    [NotMapped]
    public static string MainCoverPath { get; } = "https://blobstopify.blob.core.windows.net/album-covers/";

    [Required(ErrorMessage = "Id error!")]
    public int Id { get; set; }

    [StringLength(50, ErrorMessage = "Maximum length is 50!")]
    public string Title { get; set; } = null!;

    public DateOnly ReleaseDate { get; set; }

    [StringLength(2048)]
    public string Cover { get; set; } = null!;

    public int Saves { get; set; }

    public int Songs { get; set; }

    public int Duration { get; set; }

    public virtual ICollection<Song> SongsNavigation { get; set; } = new List<Song>();

    public virtual ICollection<UserAlbum> UserAlbums { get; set; } = new List<UserAlbum>();

    public virtual ICollection<Artist> Artists { get; set; } = new List<Artist>();
}
