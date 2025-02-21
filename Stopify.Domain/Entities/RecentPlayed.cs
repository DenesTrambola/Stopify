using Stopify.Domain.Contracts.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stopify.Domain.Entities;

public partial class RecentPlayed : IEntity
{
    public RecentPlayed(int userId, int songId, int position)
    {
        UserId = userId;
        SongId = songId;
        Position = position;
    }

    [Required(ErrorMessage = "Id error!")]
    public int Id { get; set; }

    [ForeignKey(nameof(Entities.User))]
    public int UserId { get; set; }

    [ForeignKey(nameof(Entities.Song))]
    public int SongId { get; set; }

    public int Position { get; set; }

    public virtual Song Song { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
