using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Contracts.Services;
using Stopify.Domain.Entities;
using Stopify.Exceptions.ValidationExceptions;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Stopify.Domain.Services;

public class UserFollowerService : IUserFollowerService
{
    private readonly IUnitOfWork _unit;

    public UserFollowerService(IUnitOfWork unit) =>
        _unit = unit;

    public async Task CreateAsync(UserFollower entity)
    {
        var user = await _unit.Users.GetByIdAsync(entity.UserId);
        var follower = await _unit.Users.GetByIdAsync(entity.FollowerId);

        if (user == null)
            throw new EntityNotFoundException(nameof(User));
        if (follower == null)
            throw new EntityNotFoundException(nameof(User));

        var userFollowerItem = await GetAsync(user.Id, follower.Id);
        if (userFollowerItem is not null)
            throw new EntityAlreadyExistsException(nameof(UserFollower));

        await _unit.UserFollowers.AddAsync(entity);
        await _unit.SaveChangesAsync();
    }

    public async Task<IEnumerable<UserFollower>?> GetAllAsync() =>
        await _unit.UserFollowers.GetAllAsync();

    public async Task<IEnumerable<UserFollower>?> GetAllAsync(Expression<Func<UserFollower, bool>>? expression) =>
        await _unit.UserFollowers.GetAllAsync(expression);

    public async Task<IEnumerable<UserFollower>?> GetAllByFollowerIdAsync(int followerId, Expression<Func<UserFollower, bool>>? expression = null) =>
        await _unit.UserFollowers.GetAllByFollowerIdAsync(followerId, expression);

    public async Task<IEnumerable<UserFollower>?> GetAllByFollowedDateAsync(DateTime followedDate, Expression<Func<UserFollower, bool>>? expression = null) =>
        await _unit.UserFollowers.GetAllByFollowedDateAsync(followedDate, expression);

    public async Task<IEnumerable<UserFollower>?> GetAllByUserIdAsync(int userId, Expression<Func<UserFollower, bool>>? expression = null) =>
        await _unit.UserFollowers.GetAllByUserIdAsync(userId, expression);

    public async Task<UserFollower> GetAsync(int userId, int followerId, Expression<Func<UserFollower, bool>>? expression = null)
    {
        var userFollowerByUser = await _unit.UserFollowers.GetAllByUserIdAsync(userId);
        var userFollowerByFollower = await _unit.UserFollowers.GetAllByFollowerIdAsync(followerId);

        var commonObjects = userFollowerByUser.Intersect(userFollowerByFollower);
        if (!commonObjects.Any())
            throw new EntityNotFoundException(nameof(UserFollower));

        return commonObjects.First()!;
    }

    public async Task<UserFollower?> GetFirstByFollowerIdAsync(int followerId, Expression<Func<UserFollower, bool>>? expression = null) =>
        await _unit.UserFollowers.GetFirstByFollowerIdAsync(followerId, expression);

    public async Task<UserFollower?> GetFirstByFollowedDateAsync(DateTime followedDate, Expression<Func<UserFollower, bool>>? expression = null) =>
        await _unit.UserFollowers.GetFirstByFollowedDateAsync(followedDate, expression);

    public async Task<UserFollower?> GetFirstByUserIdAsync(int userId, Expression<Func<UserFollower, bool>>? expression = null) =>
        await _unit.UserFollowers.GetFirstByUserIdAsync(userId, expression);

    public async Task RemoveAsync(UserFollower dto)
    {
        var user = await _unit.Users.GetByIdAsync(dto.UserId);
        var follower = await _unit.Users.GetByIdAsync(dto.FollowerId);

        if (user == null)
            throw new EntityNotFoundException(nameof(User));
        if (follower == null)
            throw new EntityNotFoundException(nameof(User));

        var userFollowerItem = await GetAsync(user.Id, follower.Id);
        if (userFollowerItem is null)
            throw new EntityNotFoundException(nameof(UserFollower));

        _unit.UserFollowers.Remove(userFollowerItem);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateFollowerAsync(UserFollower userFollowerItem, [StringLength(50, ErrorMessage = "Maximum length is 50!")] string newFollowerUsername)
    {
        var follower = await _unit.Users.GetByUsernameAsync(newFollowerUsername);
        if (follower == null)
            throw new EntityNotFoundException(nameof(User));

        var userFollowerItemByNewFollowerUsername = await GetAsync(userFollowerItem.UserId, follower.Id);
        if (userFollowerItemByNewFollowerUsername != null)
            throw new EntityAlreadyExistsException(nameof(UserFollower));

        userFollowerItem.FollowerId = follower.Id;

        _unit.UserFollowers.Update(userFollowerItem, e => e.FollowerId);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateFollowedDateAsync(UserFollower userFollowerItem, DateTime newFollowedDate)
    {
        if (userFollowerItem.FollowedDate == newFollowedDate)
            throw new SamePropertyNameException(nameof(UserFollower) + " " + nameof(UserFollower.FollowedDate));
        userFollowerItem.FollowedDate = newFollowedDate;

        _unit.UserFollowers.Update(userFollowerItem, e => e.FollowedDate);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(UserFollower userFollowerItem, [StringLength(50, ErrorMessage = "Maximum length is 50!")] string newUsername)
    {
        var user = await _unit.Users.GetByUsernameAsync(newUsername);
        if (user == null)
            throw new EntityNotFoundException(nameof(User));

        var userFollowerItemByNewUsername = await GetAsync(user.Id, userFollowerItem.FollowerId);
        if (userFollowerItemByNewUsername != null)
            throw new EntityAlreadyExistsException(nameof(UserFollower));

        userFollowerItem.UserId = user.Id;

        _unit.UserFollowers.Update(userFollowerItem, e => e.UserId);
        await _unit.SaveChangesAsync();
    }
}
