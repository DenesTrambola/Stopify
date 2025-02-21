using System.ComponentModel.DataAnnotations;

namespace Stopify.Domain.Entities;

public partial class UserPlaylist
{
    public UserPlaylist(int userId, int playlistId)
    {
        UserId = userId;
        PlaylistId = playlistId;
        SavedDate = DateTime.Now;
    }

    [Required(ErrorMessage = "Id error!")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Id error!")]
    public int PlaylistId { get; set; }

    public DateTime SavedDate { get; set; }

    public virtual Playlist Playlist { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
