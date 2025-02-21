using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Stopify.Domain.DTOs;

public class PlaylistDto : IDto<Playlist, PlaylistDto>
{
    [StringLength(50, ErrorMessage = "Maximum length is 50!")]
    public string Title { get; set; }

    public PlaylistDto(string title) =>
        Title = title;

    public PlaylistDto MapToDto(Playlist entity) =>
        new(entity.Title);
}
