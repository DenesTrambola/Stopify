using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Stopify.Domain.DTOs;

public class ArtistDto : IDto<Artist, ArtistDto>
{
    [StringLength(50, ErrorMessage = "Maximum length is 50!")]
    public string Name { get; set; }

    public ArtistDto(string name) =>
        Name = name;

    public ArtistDto MapToDto(Artist entity) =>
        new(entity.Name);
}
