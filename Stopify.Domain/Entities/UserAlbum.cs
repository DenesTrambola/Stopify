using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stopify.Domain.Entities;

public partial class UserAlbum
{
    public UserAlbum(int userId, int albumId)
    {
        UserId = userId;
        AlbumId = albumId;
        SavedDate = DateTime.Now;
    }

    [Required(ErrorMessage = "Id error!")]
    [ForeignKey(nameof(Entities.User))]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Id error!")]
    [ForeignKey(nameof(Entities.Album))]
    public int AlbumId { get; set; }

    public DateTime SavedDate { get; set; }

    public virtual Album Album { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
