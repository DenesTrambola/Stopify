using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Contracts.Services;
using Stopify.Domain.DTOs;
using Stopify.Domain.Entities;
using Stopify.Exceptions.ValidationExceptions;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Stopify.Domain.Services;

public class PlaybackHistoryService : IPlaybackHistoryService
{
    private readonly IUnitOfWork _unit;

    public PlaybackHistoryService(IUnitOfWork unit) =>
        _unit = unit;

    public async Task CreateAsync(PlaybackHistory entity)
    {
        var user = await _unit.Users.GetByIdAsync(entity.UserId);
        var song = await _unit.Songs.GetByIdAsync(entity.SongId);

        if (user == null)
            throw new EntityNotFoundException(nameof(User));
        if (song == null)
            throw new EntityNotFoundException(nameof(Song));

        var playbackHistoryItem = GetAsync(user.Id, song.Id, entity.Position);
        if (playbackHistoryItem is not null)
            throw new EntityAlreadyExistsException(nameof(PlaybackHistory));

        await _unit.PlaybackHistories.AddAsync(entity);
        await _unit.SaveChangesAsync();
    }

    public async Task<IEnumerable<PlaybackHistory>?> GetAllAsync(Expression<Func<PlaybackHistory, bool>>? expression = null) =>
        await _unit.PlaybackHistories.GetAllAsync(expression);

    public async Task<IEnumerable<PlaybackHistory>?> GetAllByPlaybackDateTimeAsync(DateTime playbackDateTime, Expression<Func<PlaybackHistory, bool>>? expression = null) =>
        await _unit.PlaybackHistories.GetAllByPlaybackDateTimeAsync(playbackDateTime, expression);

    public async Task<IEnumerable<PlaybackHistory>?> GetAllByPositionAsync(int position, Expression<Func<PlaybackHistory, bool>>? expression = null) =>
        await _unit.PlaybackHistories.GetAllByPositionAsync(position, expression);

    public async Task<IEnumerable<PlaybackHistory>?> GetAllBySongIdAsync(int songId, Expression<Func<PlaybackHistory, bool>>? expression = null) =>
        await _unit.PlaybackHistories.GetAllBySongIdAsync(songId, expression);

    public async Task<IEnumerable<PlaybackHistory>?> GetAllByUserIdAsync(int userId, Expression<Func<PlaybackHistory, bool>>? expression = null) =>
        await _unit.PlaybackHistories.GetAllByUserIdAsync(userId, expression);

    public async Task<PlaybackHistory?> GetAsync(int userId, int songId, int position, Expression<Func<PlaybackHistory, bool>>? expression = null)
    {
        var playbackHistoriesByUser = await _unit.PlaybackHistories.GetAllByUserIdAsync(userId);
        var playbackHistoriesBySong = await _unit.PlaybackHistories.GetAllBySongIdAsync(songId);
        var playbackHistoriesByPosition = await _unit.PlaybackHistories.GetAllByPositionAsync(position);

        var commonObjects = playbackHistoriesByUser.Intersect(playbackHistoriesBySong).Intersect(playbackHistoriesByPosition);
        return commonObjects.Any() ? commonObjects.First() : null;
    }

    public async Task<PlaybackHistory?> GetByIdAsync(int id, Expression<Func<PlaybackHistory, bool>>? expression = null) =>
        await _unit.PlaybackHistories.GetByIdAsync(id, expression);

    public async Task<PlaybackHistory?> GetFirstByPlaybackDateTimeAsync(DateTime playbackDateTime, Expression<Func<PlaybackHistory, bool>>? expression = null) =>
        await _unit.PlaybackHistories.GetFirstByPlaybackDateTimeAsync(playbackDateTime, expression);

    public async Task<PlaybackHistory?> GetFirstByPositionAsync(int position, Expression<Func<PlaybackHistory, bool>>? expression = null) =>
        await _unit.PlaybackHistories.GetFirstByPositionAsync(position, expression);

    public async Task<PlaybackHistory?> GetFirstBySongIdAsync(int songId, Expression<Func<PlaybackHistory, bool>>? expression = null) =>
        await _unit.PlaybackHistories.GetFirstBySongIdAsync(songId, expression);

    public async Task<PlaybackHistory?> GetFirstByUserIdAsync(int userId, Expression<Func<PlaybackHistory, bool>>? expression = null) =>
        await _unit.PlaybackHistories.GetFirstByUserIdAsync(userId, expression);

    public async Task RemoveAsync(PlaybackHistoryDto dto)
    {
        var user = await _unit.Users.GetByUsernameAsync(dto.Username);
        var song = await _unit.Songs.GetByTitleAsync(dto.SongTitle);

        if (user == null)
            throw new EntityNotFoundException(nameof(User));
        if (song == null)
            throw new EntityNotFoundException(nameof(Song));

        var playbackHistoryItem = await GetAsync(user.Id, song.Id, dto.Position);
        if (playbackHistoryItem is null)
            throw new EntityNotFoundException(nameof(PlaybackHistory));

        _unit.PlaybackHistories.Remove(playbackHistoryItem);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdatePositionAsync(PlaybackHistory historyItem, int newPosition)
    {
        if (historyItem.Position == newPosition)
            throw new SamePropertyNameException(nameof(PlaybackHistory) + " " + nameof(PlaybackHistory.Position));
        historyItem.Position = newPosition;

        _unit.PlaybackHistories.Update(historyItem, e => e.Position);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateSongAsync(PlaybackHistory historyItem, [StringLength(200, ErrorMessage = "Maximum length is 200!")] string newSongTitle)
    {
        var song = await _unit.Songs.GetByTitleAsync(newSongTitle);
        if (song == null)
            throw new EntityNotFoundException(nameof(Song));

        var historyItemByNewSongTitle = await GetAsync(historyItem.UserId, song.Id, historyItem.Position);
        if (historyItemByNewSongTitle != null)
            throw new EntityAlreadyExistsException(nameof(PlaybackHistory));

        historyItem.SongId = song.Id;

        _unit.PlaybackHistories.Update(historyItem, e => e.SongId);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(PlaybackHistory historyItem, [StringLength(50, ErrorMessage = "Maximum length is 50!")] string newUsername)
    {
        var user = await _unit.Users.GetByUsernameAsync(newUsername);
        if (user == null)
            throw new EntityNotFoundException(nameof(User));

        var historyItemByNewUsername = await GetAsync(user.Id, historyItem.SongId, historyItem.Position);
        if (historyItemByNewUsername != null)
            throw new EntityAlreadyExistsException(nameof(PlaybackHistory));

        historyItem.UserId = user.Id;

        _unit.PlaybackHistories.Update(historyItem, e => e.UserId);
        await _unit.SaveChangesAsync();
    }
}
