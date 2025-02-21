using System.ComponentModel.DataAnnotations;
using Stopify.Domain.Contracts.Common;

namespace Stopify.Domain.Entities;

public partial class Genre : IEntity
{
    public Genre(string name) =>
        Name = name;

    [Required(ErrorMessage = "Id error!")]
    public int Id { get; set; }

    [StringLength(50, ErrorMessage = "Maximum length is 50!")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
