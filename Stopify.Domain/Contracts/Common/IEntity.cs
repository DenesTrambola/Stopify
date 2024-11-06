using System.ComponentModel.DataAnnotations;

namespace Stopify.Domain.Contracts.Common;

public interface IEntity
{
    [Required(ErrorMessage = "Id error!")]
    int Id { get; set; }
}
