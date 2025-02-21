using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Contracts.Services;
using Stopify.Domain.DTOs;
using Stopify.Domain.Entities;
using Stopify.Exceptions.ValidationExceptions;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Stopify.Domain.Services;

public class QueueService : IQueueService
{
    private readonly IUnitOfWork _unit;

    public QueueService(IUnitOfWork unit) =>
        _unit = unit;

    public async Task CreateAsync(SongQueue entity)
    {
        var user = await _unit.Users.GetByIdAsync(entity.UserId);
        var song = await _unit.Songs.GetByIdAsync(entity.SongId);

        if (user == null)
            throw new EntityNotFoundException(nameof(User));
        if (song == null)
            throw new EntityNotFoundException(nameof(Song));

        var queueItem = await GetAsync(user.Id, song.Id, entity.Position);
        if (queueItem is not null)
            throw new EntityAlreadyExistsException(nameof(SongQueue));

        await _unit.Queues.AddAsync(entity);
        await _unit.SaveChangesAsync();
    }

    public async Task<IEnumerable<SongQueue>?> GetAllAsync() =>
        await _unit.Queues.GetAllAsync();

    public async Task<IEnumerable<SongQueue>?> GetAllAsync(Expression<Func<SongQueue, bool>>? expression) =>
        await _unit.Queues.GetAllAsync(expression);

    public async Task<IEnumerable<SongQueue>?> GetAllByPositionAsync(int position, Expression<Func<SongQueue, bool>>? expression = null) =>
        await _unit.Queues.GetAllByPositionAsync(position, expression);

    public async Task<IEnumerable<SongQueue>?> GetAllBySongIdAsync(int songId, Expression<Func<SongQueue, bool>>? expression = null) =>
        await _unit.Queues.GetAllBySongIdAsync(songId, expression);

    public async Task<IEnumerable<SongQueue>?> GetAllByUserIdAsync(int userId, Expression<Func<SongQueue, bool>>? expression = null) =>
        await _unit.Queues.GetAllByUserIdAsync(userId, expression);

    public async Task<SongQueue?> GetAsync(int userId, int songId, int position, Expression<Func<PlaybackHistory, bool>>? expression = null)
    {
        var queuesByUser = await _unit.Queues.GetAllByUserIdAsync(userId);
        var queuesBySong = await _unit.Queues.GetAllBySongIdAsync(songId);
        var queuesByPosition = await _unit.Queues.GetAllByPositionAsync(position);

        var commonObjects = queuesByUser.Intersect(queuesBySong).Intersect(queuesByPosition);
        return commonObjects.Any() ? commonObjects.First() : null;
    }

    public async Task<SongQueue?> GetByIdAsync(int id, Expression<Func<SongQueue, bool>>? expression = null) =>
        await _unit.Queues.GetByIdAsync(id, expression);

    public async Task<SongQueue?> GetFirstByPositionAsync(int position, Expression<Func<SongQueue, bool>>? expression = null) =>
        await _unit.Queues.GetFirstByPositionAsync(position, expression);

    public async Task<SongQueue?> GetFirstBySongIdAsync(int songId, Expression<Func<SongQueue, bool>>? expression = null) =>
        await _unit.Queues.GetFirstBySongIdAsync(songId, expression);

    public async Task<SongQueue?> GetFirstByUserIdAsync(int userId, Expression<Func<SongQueue, bool>>? expression = null) =>
        await _unit.Queues.GetFirstByUserIdAsync(userId, expression);

    public async Task RemoveAsync(QueueDto dto)
    {
        var user = await _unit.Users.GetByUsernameAsync(dto.Username);
        var song = await _unit.Songs.GetByTitleAsync(dto.SongTitle);

        if (user == null)
            throw new EntityNotFoundException(nameof(User));
        if (song == null)
            throw new EntityNotFoundException(nameof(Song));

        var queueItem = await GetAsync(user.Id, song.Id, dto.Position);
        if (queueItem is null)
            throw new EntityNotFoundException(nameof(SongQueue));

        _unit.Queues.Remove(queueItem);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdatePositionAsync(SongQueue queueItem, int newPosition)
    {
        if (queueItem.Position == newPosition)
            throw new SamePropertyNameException(nameof(SongQueue) + " " + nameof(SongQueue.Position));
        queueItem.Position = newPosition;

        _unit.Queues.Update(queueItem, e => e.Position);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateSongAsync(SongQueue queueItem, [StringLength(200, ErrorMessage = "Maximum length is 200!")] string newSongTitle)
    {
        var song = await _unit.Songs.GetByTitleAsync(newSongTitle);
        if (song == null)
            throw new EntityNotFoundException(nameof(Song));

        var queueItemBySongTitle = await GetAsync(queueItem.UserId, song.Id, queueItem.Position);
        if (queueItemBySongTitle != null)
            throw new EntityAlreadyExistsException(nameof(SongQueue));

        queueItem.SongId = song.Id;

        _unit.Queues.Update(queueItem, e => e.SongId);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(SongQueue queueItem, [StringLength(50, ErrorMessage = "Maximum length is 50!")] string newUsername)
    {
        var user = await _unit.Users.GetByUsernameAsync(newUsername);
        if (user == null)
            throw new EntityAlreadyExistsException(nameof(SongQueue));

        var queueItemByUsername = await GetAsync(user.Id, queueItem.SongId, queueItem.Position);
        if (queueItemByUsername != null)
            throw new EntityAlreadyExistsException(nameof(SongQueue));

        queueItem.UserId = user.Id;

        _unit.Queues.Update(queueItem, e => e.UserId);
        await _unit.SaveChangesAsync();
    }
}
