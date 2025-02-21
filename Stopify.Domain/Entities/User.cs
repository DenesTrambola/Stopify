using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Other;
using Stopify.Exceptions.ValidationExceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stopify.Domain.Entities;

public partial class User : IEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class with the specified hashed password.
    /// </summary>
    /// <remarks>
    /// This constructor should only be used when a hashed password is already available.
    /// For better security, use the <see cref="Create"/> method to hash the password before creating an instance.
    /// </remarks>
    /// <param name="username">The username of the user.</param>
    /// <param name="firstName">The first name of the user.</param>
    /// <param name="lastName">The last name of the user.</param>
    /// <param name="email">The email address of the user. Must be in a valid email format.</param>
    /// <param name="passwordHash">The hashed password of the user. Ensure this is securely hashed.</param>
    /// <exception cref="InvalidEmailException">Thrown if the email format is invalid.</exception>
    public User(string username, string firstName, string lastName, string email, string passwordHash, string? avatar = null)
    {
        if (!EmailValidation.CheckFormat(email))
            throw new InvalidEmailException();

        Username = username;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        DateJoined = DateTime.Now;
        Avatar = avatar is null ? $"{MainAvatarPath}user-avatar-default.png" :
            UrlValidation.CheckFormat(avatar) ? avatar : $"{MainAvatarPath}{avatar}.png";
    }

    /// <summary>
    /// Creates a new <see cref="User"/> instance by hashing the provided password.
    /// </summary>
    /// <param name="username">The username of the user.</param>
    /// <param name="firstName">The first name of the user.</param>
    /// <param name="lastName">The last name of the user.</param>
    /// <param name="email">The email address of the user. Must be in a valid email format.</param>
    /// <param name="password">The plain-text password of the user, which will be hashed.</param>
    /// <returns>A <see cref="User"/> instance with a securely hashed password.</returns>
    /// <exception cref="InvalidEmailException">Thrown if the email format is invalid.</exception>
    public static User Create(string username, string firstName, string lastName, string email, string password, string? avatar = null) =>
        new User(username, firstName, lastName, email, BCrypt.Net.BCrypt.HashPassword(password), avatar);

    [NotMapped]
    public static string MainAvatarPath { get; } = "https://blobstopify.blob.core.windows.net/user-avatars/";

    [Required(ErrorMessage = "Id error!")]
    public int Id { get; set; }

    [StringLength(50, ErrorMessage = "Maximum length is 50!")]
    public string Username { get; set; } = null!;

    [StringLength(50, ErrorMessage = "Maximum length is 50!")]
    public string FirstName { get; set; } = null!;

    [StringLength(50, ErrorMessage = "Maximum length is 50!")]
    public string LastName { get; set; } = null!;

    [StringLength(254, ErrorMessage = "Maximum length is 254!")]
    [EmailAddress(ErrorMessage = "Invalid format!")]
    public string Email { get; set; } = null!;

    [StringLength(60, ErrorMessage = "Maximum length is 60!")]
    public string PasswordHash { get; set; } = null!;

    public DateTime DateJoined { get; set; }

    [StringLength(2048, ErrorMessage = "Maximum length is 2048!")]
    public string Avatar { get; set; } = null!;

    public virtual ICollection<SongQueue> Queues { get; set; } = new List<SongQueue>();

    public virtual ICollection<RecentPlayed> RecentPlays { get; set; } = new List<RecentPlayed>();

    public virtual ICollection<UserAlbum> UserAlbums { get; set; } = new List<UserAlbum>();

    public virtual ICollection<UserArtist> UserArtists { get; set; } = new List<UserArtist>();

    public virtual ICollection<PlaybackHistory> PlaybackHistories { get; set; } = new List<PlaybackHistory>();

    public virtual ICollection<UserPlaylist> UserPlaylists { get; set; } = new List<UserPlaylist>();

    public virtual ICollection<UserFollower> Followers { get; set; } = new List<UserFollower>();

    public virtual ICollection<UserFollower> Followings { get; set; } = new List<UserFollower>();
}
