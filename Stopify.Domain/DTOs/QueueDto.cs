using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Stopify.Domain.DTOs;

public class QueueDto : IDto<SongQueue, QueueDto>
{
    [StringLength(50, ErrorMessage = "Maximum length is 50!")]
    public string Username { get; set; }

    [StringLength(50, ErrorMessage = "Maximum length is 50!")]
    public string SongTitle { get; set; }

    public int Position { get; set; }

    public QueueDto(string username, string songTitle, int position)
    {
        Username = username;
        SongTitle = songTitle;
        Position = position;
    }

    public QueueDto MapToDto(SongQueue entity) =>
        new(entity.User.Username, entity.Song.Title, entity.Position);
}
