using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stopify.Domain.Entities;

public partial class SongPlaylist
{
    public SongPlaylist(int songId, int playlistId, int position)
    {
        SongId = songId;
        PlaylistId = playlistId;
        Position = position;
    }

    [Required(ErrorMessage = "Id error!")]
    [ForeignKey(nameof(Entities.Song))]
    public int SongId { get; set; }

    [Required(ErrorMessage = "Id error!")]
    [ForeignKey(nameof(Entities.Playlist))]
    public int PlaylistId { get; set; }

    public int Position { get; set; }

    public virtual Playlist Playlist { get; set; } = null!;

    public virtual Song Song { get; set; } = null!;
}
