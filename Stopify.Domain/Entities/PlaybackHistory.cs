using Stopify.Domain.Contracts.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stopify.Domain.Entities;

public partial class PlaybackHistory : IEntity
{
    public PlaybackHistory(int userId, int songId, int position)
    {
        UserId = userId;
        SongId = songId;
        Position = position;
        PlaybackDateTime = DateTime.Now;
    }

    [Required(ErrorMessage = "Id error!")]
    public int Id { get; set; }

    [ForeignKey(nameof(Entities.User))]
    public int UserId { get; set; }

    public int SongId { get; set; }

    public DateTime PlaybackDateTime { get; set; }

    public int Position { get; set; }

    public virtual Song Song { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
