using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Stopify.Domain.DTOs;

public class SongDto : IDto<Song, SongDto>
{
    [StringLength(200, ErrorMessage = "Maximum length is 200!")]
    public string Title { get; set; }

    public SongDto(string title) =>
        Title = title;

    public SongDto MapToDto(Song entity) =>
        new(entity.Title);
}
