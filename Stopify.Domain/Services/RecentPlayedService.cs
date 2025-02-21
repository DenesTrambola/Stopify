using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Contracts.Services;
using Stopify.Domain.DTOs;
using Stopify.Domain.Entities;
using Stopify.Exceptions.ValidationExceptions;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Stopify.Domain.Services;

public class RecentPlayedService : IRecentPlayedService
{
    private readonly IUnitOfWork _unit;

    public RecentPlayedService(IUnitOfWork unit) =>
        _unit = unit;

    public async Task CreateAsync(RecentPlayed entity)
    {
        var user = await _unit.Users.GetByIdAsync(entity.UserId);
        var song = await _unit.Songs.GetByIdAsync(entity.SongId);

        if (user == null)
            throw new EntityNotFoundException(nameof(User));
        if (song == null)
            throw new EntityNotFoundException(nameof(Song));

        var recentPlayedItem = await GetAsync(user.Id, song.Id, entity.Position);
        if (recentPlayedItem is not null)
            throw new EntityAlreadyExistsException(nameof(RecentPlayed));

        await _unit.RecentPlays.AddAsync(entity);
        await _unit.SaveChangesAsync();
    }

    public async Task<IEnumerable<RecentPlayed>?> GetAllAsync() =>
        await _unit.RecentPlays.GetAllAsync();

    public async Task<IEnumerable<RecentPlayed>?> GetAllAsync(Expression<Func<RecentPlayed, bool>>? expression) =>
        await _unit.RecentPlays.GetAllAsync(expression);

    public async Task<IEnumerable<RecentPlayed>?> GetAllByPositionAsync(int position, Expression<Func<RecentPlayed, bool>>? expression = null) =>
        await _unit.RecentPlays.GetAllByPositionAsync(position, expression);

    public async Task<IEnumerable<RecentPlayed>?> GetAllBySongIdAsync(int songId, Expression<Func<RecentPlayed, bool>>? expression = null) =>
        await _unit.RecentPlays.GetAllBySongIdAsync(songId, expression);

    public async Task<IEnumerable<RecentPlayed>?> GetAllByUserIdAsync(int userId, Expression<Func<RecentPlayed, bool>>? expression = null) =>
        await _unit.RecentPlays.GetAllByUserIdAsync(userId, expression);

    public async Task<RecentPlayed?> GetAsync(int userId, int songId, int position, Expression<Func<SongPlaylist, bool>>? expression = null)
    {
        var playbackHistoriesByUser = await _unit.RecentPlays.GetAllByUserIdAsync(userId);
        var playbackHistoriesBySong = await _unit.RecentPlays.GetAllBySongIdAsync(songId);
        var playbackHistoriesByPosition = await _unit.RecentPlays.GetAllByPositionAsync(position);

        var commonObjects = playbackHistoriesByUser.Intersect(playbackHistoriesBySong).Intersect(playbackHistoriesByPosition);
        return commonObjects.Any() ? commonObjects.First() : null;
    }

    public async Task<RecentPlayed?> GetByIdAsync(int id, Expression<Func<RecentPlayed, bool>>? expression = null) =>
        await _unit.RecentPlays.GetByIdAsync(id, expression);

    public async Task<RecentPlayed?> GetFirstByPositionAsync(int position, Expression<Func<RecentPlayed, bool>>? expression = null) =>
        await _unit.RecentPlays.GetFirstByPositionAsync(position, expression);

    public async Task<RecentPlayed?> GetFirstBySongIdAsync(int songId, Expression<Func<RecentPlayed, bool>>? expression = null) =>
        await _unit.RecentPlays.GetFirstBySongIdAsync(songId, expression);

    public async Task<RecentPlayed?> GetFirstByUserIdAsync(int userId, Expression<Func<RecentPlayed, bool>>? expression = null) =>
        await _unit.RecentPlays.GetFirstByUserIdAsync(userId, expression);

    public async Task RemoveAsync(RecentPlayedDto dto)
    {
        var user = await _unit.Users.GetByUsernameAsync(dto.Username);
        var song = await _unit.Songs.GetByTitleAsync(dto.SongTitle);

        if (user == null)
            throw new EntityNotFoundException(nameof(User));
        if (song == null)
            throw new EntityNotFoundException(nameof(Song));

        var recentPlayedItem = await GetAsync(user.Id, song.Id, dto.Position);
        if (recentPlayedItem is null)
            throw new EntityNotFoundException(nameof(RecentPlayed));

        _unit.RecentPlays.Remove(recentPlayedItem);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdatePositionAsync(RecentPlayed recentItem, int newPosition)
    {
        if (recentItem.Position == newPosition)
            throw new SamePropertyNameException(nameof(RecentPlayed) + " " + nameof(RecentPlayed.Position));
        recentItem.Position = newPosition;

        _unit.RecentPlays.Update(recentItem, e => e.Position);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateSongAsync(RecentPlayed recentItem, [StringLength(200, ErrorMessage = "Maximum length is 200!")] string newSongTitle)
    {
        var song = await _unit.Songs.GetByTitleAsync(newSongTitle);
        if (song == null)
            throw new EntityNotFoundException(nameof(Song));

        var recentItemByNewSongTitle = await GetAsync(recentItem.UserId, song.Id, recentItem.Position);
        if (recentItemByNewSongTitle != null)
            throw new EntityAlreadyExistsException(nameof(RecentPlayed));

        recentItem.SongId = song.Id;

        _unit.RecentPlays.Update(recentItem, e => e.SongId);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(RecentPlayed recentItem, [StringLength(50, ErrorMessage = "Maximum length is 50!")] string newUsername)
    {
        var user = await _unit.Users.GetByUsernameAsync(newUsername);
        if (user == null)
            throw new EntityNotFoundException(nameof(User));

        var recentItemByNewUsername = await GetAsync(user.Id, recentItem.SongId, recentItem.Position);
        if (recentItemByNewUsername != null)
            throw new EntityAlreadyExistsException(nameof(RecentPlayed));

        recentItem.UserId = user.Id;

        _unit.RecentPlays.Update(recentItem, e => e.UserId);
        await _unit.SaveChangesAsync();
    }
}
