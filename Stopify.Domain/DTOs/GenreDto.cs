using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Stopify.Domain.DTOs;

public class GenreDto : IDto<Genre, GenreDto>
{
    [StringLength(50, ErrorMessage = "Maximum length is 50!")]
    public string Name { get; set; }

    public GenreDto(string name) =>
        Name = name;

    public GenreDto MapToDto(Genre entity) =>
        new(entity.Name);
}
