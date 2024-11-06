using Stopify.Domain.Contracts.Common;
using Stopify.Domain.DTOs;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Services;

public interface IPlaybackHistoryService : IEntityService<PlaybackHistory, PlaybackHistoryDto>
{
    Task<PlaybackHistory?> GetAsync(int userId, int songId, int position, Expression<Func<PlaybackHistory, bool>>? expression = null);
    Task<PlaybackHistory?> GetFirstByUserIdAsync(int userId, Expression<Func<PlaybackHistory, bool>>? expression = null);
    Task<PlaybackHistory?> GetFirstBySongIdAsync(int songId, Expression<Func<PlaybackHistory, bool>>? expression = null);
    Task<PlaybackHistory?> GetFirstByPlaybackDateTimeAsync(DateTime playbackDateTime, Expression<Func<PlaybackHistory, bool>>? expression = null);
    Task<PlaybackHistory?> GetFirstByPositionAsync(int position, Expression<Func<PlaybackHistory, bool>>? expression = null);

    Task<IEnumerable<PlaybackHistory>?> GetAllByUserIdAsync(int userId, Expression<Func<PlaybackHistory, bool>>? expression = null);
    Task<IEnumerable<PlaybackHistory>?> GetAllBySongIdAsync(int songId, Expression<Func<PlaybackHistory, bool>>? expression = null);
    Task<IEnumerable<PlaybackHistory>?> GetAllByPlaybackDateTimeAsync(DateTime playbackDateTime, Expression<Func<PlaybackHistory, bool>>? expression = null);
    Task<IEnumerable<PlaybackHistory>?> GetAllByPositionAsync(int position, Expression<Func<PlaybackHistory, bool>>? expression = null);

    Task UpdateUserAsync(PlaybackHistory historyItem, string newUsername);
    Task UpdateSongAsync(PlaybackHistory historyItem, string newSongTitle);
    Task UpdatePositionAsync(PlaybackHistory historyItem, int newPosition);
}
