﻿using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Repositories;

public interface IUserFollowerRepository : IRepository<UserFollower>
{
    Task<IEnumerable<UserFollower>?> GetAllByUserIdAsync(int userId, Expression<Func<UserFollower, bool>>? expression = null);
    Task<IEnumerable<UserFollower>?> GetAllByFollowedDateAsync(DateTime followedDate, Expression<Func<UserFollower, bool>>? expression = null);
    Task<IEnumerable<UserFollower>?> GetAllByFollowerIdAsync(int followerId, Expression<Func<UserFollower, bool>>? expression = null);
    Task<UserFollower?> GetFirstByUserIdAsync(int userId, Expression<Func<UserFollower, bool>>? expression = null);
    Task<UserFollower?> GetFirstByFollowedDateAsync(DateTime followedDate, Expression<Func<UserFollower, bool>>? expression = null);
    Task<UserFollower?> GetFirstByFollowerIdAsync(int followerId, Expression<Func<UserFollower, bool>>? expression = null);
}