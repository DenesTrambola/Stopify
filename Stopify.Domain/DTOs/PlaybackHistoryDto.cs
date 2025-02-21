using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Stopify.Domain.DTOs;

public class PlaybackHistoryDto : IDto<PlaybackHistory, PlaybackHistoryDto>
{
    [StringLength(50, ErrorMessage = "Maximum length is 50!")]
    public string Username { get; set; }

    [StringLength(200, ErrorMessage = "Maximum length is 200!")]
    public string SongTitle { get; set; }

    public int Position { get; set; }

    public PlaybackHistoryDto(string username, string songTitle, int position)
    {
        Username = username;
        SongTitle = songTitle;
        Position = position;
    }

    public PlaybackHistoryDto MapToDto(PlaybackHistory entity) =>
        new(entity.User.Username, entity.Song.Title, entity.Position);
}
