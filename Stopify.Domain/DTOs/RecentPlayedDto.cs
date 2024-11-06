using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Stopify.Domain.DTOs;

public class RecentPlayedDto : IDto<RecentPlayed, RecentPlayedDto>
{
    [StringLength(50, ErrorMessage = "Maximum length is 50!")]
    public string Username { get; set; }

    [StringLength(50, ErrorMessage = "Maximum length is 50!")]
    public string SongTitle { get; set; }

    public int Position { get; set; }

    public RecentPlayedDto(string username, string songTitle, int position)
    {
        Username = username;
        SongTitle = songTitle;
        Position = position;
    }

    public RecentPlayedDto MapToDto(RecentPlayed entity) =>
        new(entity.User.Username, entity.Song.Title, entity.Position);
}
