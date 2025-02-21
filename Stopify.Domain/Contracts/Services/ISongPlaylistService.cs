using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Services;

public interface ISongPlaylistService : IService<SongPlaylist, SongPlaylist>
{
    Task<SongPlaylist> GetAsync(int songId, int playlistId, Expression<Func<SongPlaylist, bool>>? expression = null);
    Task<SongPlaylist?> GetFirstBySongIdAsync(int songId, Expression<Func<SongPlaylist, bool>>? expression = null);
    Task<SongPlaylist?> GetFirstByPlaylistIdAsync(int playlistId, Expression<Func<SongPlaylist, bool>>? expression = null);
    Task<SongPlaylist?> GetFirstByPositionAsync(int position, Expression<Func<SongPlaylist, bool>>? expression = null);

    Task<IEnumerable<SongPlaylist>?> GetAllBySongIdAsync(int songId, Expression<Func<SongPlaylist, bool>>? expression = null);
    Task<IEnumerable<SongPlaylist>?> GetAllByPlaylistIdAsync(int playlistId, Expression<Func<SongPlaylist, bool>>? expression = null);
    Task<IEnumerable<SongPlaylist>?> GetAllByPositionAsync(int position, Expression<Func<SongPlaylist, bool>>? expression = null);

    Task UpdateSongAsync(SongPlaylist songPlaylistItem, string newSongTitle);
    Task UpdatePlaylistAsync(SongPlaylist songPlaylistItem, string newPlaylistTitle, int? position = null);
    Task UpdatePositionAsync(SongPlaylist songPlaylistItem, int newPosition);

    Task CreateAsync(string songTitle, string playlistTitle, int? position = null);
}
