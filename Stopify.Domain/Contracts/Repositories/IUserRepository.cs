using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Repositories;

public interface IUserRepository : IEntityRepository<User>
{
    Task<User?> GetByUsernameAsync(string username, Expression<Func<User, bool>>? expression = null);
    Task<User?> GetFirstByFirstNameAsync(string firstName, Expression<Func<User, bool>>? expression = null);
    Task<User?> GetFirstByLastNameAsync(string lastName, Expression<Func<User, bool>>? expression = null);
    Task<User?> GetFirstByFullNameAsync(string firstName, string lastName, Expression<Func<User, bool>>? expression = null);
    Task<User?> GetByEmailAsync(string email, Expression<Func<User, bool>>? expression = null);
    Task<User?> GetFirstByPasswordHashAsync(string passwordHash, Expression<Func<User, bool>>? expression = null);
    Task<User?> GetFirstByDateJoinedAsync(DateTime dateJoined, Expression<Func<User, bool>>? expression = null);
    Task<User?> GetFirstByAvatarAsync(string avatar, Expression<Func<User, bool>>? expression = null);

    Task<IEnumerable<User>?> GetAllByFirstNameAsync(string firstName, Expression<Func<User, bool>>? expression = null);
    Task<IEnumerable<User>?> GetAllByLastNameAsync(string lastName, Expression<Func<User, bool>>? expression = null);
    Task<IEnumerable<User>?> GetAllByFullNameAsync(string firstName, string lastName, Expression<Func<User, bool>>? expression = null);
    Task<IEnumerable<User>?> GetAllByPasswordHashAsync(string passwordHash, Expression<Func<User, bool>>? expression = null);
    Task<IEnumerable<User>?> GetAllByDateJoinedAsync(DateTime dateJoined, Expression<Func<User, bool>>? expression = null);
    Task<IEnumerable<User>?> GetAllByAvatarAsync(string avatar, Expression<Func<User, bool>>? expression = null);
}
