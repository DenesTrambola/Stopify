using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stopify.Domain.Entities;

public partial class UserArtist
{
    public UserArtist(int userId, int artistId)
    {
        UserId = userId;
        ArtistId = artistId;
        FollowedDate = DateTime.Now;
    }

    [Required(ErrorMessage = "Id error!")]
    [ForeignKey(nameof(Entities.User))]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Id error!")]
    [ForeignKey(nameof(Entities.Artist))]
    public int ArtistId { get; set; }

    public DateTime FollowedDate { get; set; }

    public virtual Artist Artist { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
