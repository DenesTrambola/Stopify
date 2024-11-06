using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Stopify.Domain.DTOs;

public class UserDto : IDto<User, UserDto>
{
    [StringLength(50, ErrorMessage = "Maximum length is 50!")]
    public string Username { get; set; }

    public UserDto(string username) =>
        Username = username;

    public UserDto MapToDto(User entity) =>
        new(entity.Username);
}
