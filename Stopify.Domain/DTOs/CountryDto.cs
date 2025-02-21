using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Stopify.Domain.DTOs;

public class CountryDto : IDto<Country, CountryDto>
{
    [StringLength(60, ErrorMessage = "Maximum length is 60!")]
    public string Name { get; set; }

    public CountryDto(string name) =>
        Name = name;

    public CountryDto MapToDto(Country entity) =>
        new(entity.Name);
}
