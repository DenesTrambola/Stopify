using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Other;
using Stopify.Exceptions.ValidationExceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stopify.Domain.Entities;

public partial class Artist : IEntity
{
    public Artist([StringLength(50, ErrorMessage = "Maximum length is 50!")] string name,
                  bool isVerified,
                  [StringLength(2048, ErrorMessage = "Maximum length is 2048!")] string? avatar = null,
                  [StringLength(200, ErrorMessage = "Maximum length is 200!")] string? bio = null,
                  int? countryId = null,
                  [StringLength(2048, ErrorMessage = "Maximum length is 2048!")] string? bgImage = null)
    {
        Name = name;
        IsVerified = isVerified;
        Bio = bio;
        CountryId = countryId;

        if (avatar is not null)
            Avatar = UrlValidation.CheckFormat(avatar) ? avatar : $"{MainAvatarPath}{avatar}.jpg";

        if (bgImage is not null)
            BgImage = UrlValidation.CheckFormat(bgImage) ? bgImage : $"{MainBgImagePath}{bgImage}.png";

        if (!UrlValidation.CheckFormat(Avatar) && avatar is not null)
            throw new InvalidUrlException();
        if (!UrlValidation.CheckFormat(BgImage) && BgImage is not null)
            throw new InvalidUrlException();
    }

    [NotMapped]
    public static string MainAvatarPath { get; } = "https://blobstopify.blob.core.windows.net/artist-avatars/";

    [NotMapped]
    public static string MainBgImagePath { get; } = "https://blobstopify.blob.core.windows.net/artist-bgimages/";

    [Required(ErrorMessage = "Id error!")]
    public int Id { get; set; }

    [StringLength(50, ErrorMessage = "Maximum length is 50!")]
    public string Name { get; set; } = null!;

    [StringLength(200, ErrorMessage = "Maximum length is 200!")]
    public string? Bio { get; set; }

    [ForeignKey(nameof(Artist))]
    public int? CountryId { get; set; }

    [StringLength(2048, ErrorMessage = "Maximum length is 2048!")]
    public string? Avatar { get; set; }

    [StringLength(2048, ErrorMessage = "Maximum length is 2048!")]
    public string? BgImage { get; set; }

    public bool IsVerified { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<UserArtist> UserArtists { get; set; } = new List<UserArtist>();

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
