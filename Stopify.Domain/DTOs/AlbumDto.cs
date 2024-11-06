using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Stopify.Domain.DTOs;

public class AlbumDto : IDto<Album, AlbumDto>
{
    [StringLength(50, ErrorMessage = "Maximum length is 50!")]
    public string Title { get; set; }

    public AlbumDto(string title) =>
        Title = title;

    public AlbumDto MapToDto(Album entity) =>
        new(entity.Title);
}
