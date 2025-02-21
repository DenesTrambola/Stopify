using System.ComponentModel.DataAnnotations;

namespace Stopify.Domain.Entities;

public class UserFollower
{
    public UserFollower(int userId, int followerId)
    {
        UserId = userId;
        FollowerId = followerId;
        FollowedDate = DateTime.Now;
    }

    [Required(ErrorMessage = "Id error!")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Id error!")]
    public int FollowerId { get; set; }

    public DateTime FollowedDate { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual User Follower { get; set; } = null!;
}
