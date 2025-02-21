using System.ComponentModel.DataAnnotations;
using Stopify.Domain.Contracts.Common;

namespace Stopify.Domain.Entities;

public partial class Country : IEntity
{
    public Country(string name) =>
        Name = name;

    [Required(ErrorMessage = "Id error!")]
    public int Id { get; set; }

    [StringLength(60, ErrorMessage = "Maximum length is 60!")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Artist> Artists { get; set; } = new List<Artist>();
}
