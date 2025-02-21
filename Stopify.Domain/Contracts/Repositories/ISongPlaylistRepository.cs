using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Repositories;

public interface ISongPlaylistRepository : IRepository<SongPlaylist>
{
    Task<SongPlaylist?> GetFirstBySongIdAsync(int songId, Expression<Func<SongPlaylist, bool>>? expression = null);
    Task<SongPlaylist?> GetFirstByPlaylistIdAsync(int playlistId, Expression<Func<SongPlaylist, bool>>? expression = null);
    Task<SongPlaylist?> GetFirstByPositionAsync(int position, Expression<Func<SongPlaylist, bool>>? expression = null);

    Task<IEnumerable<SongPlaylist>?> GetAllBySongIdAsync(int songId, Expression<Func<SongPlaylist, bool>>? expression = null);
    Task<IEnumerable<SongPlaylist>?> GetAllByPlaylistIdAsync(int playlistId, Expression<Func<SongPlaylist, bool>>? expression = null);
    Task<IEnumerable<SongPlaylist>?> GetAllByPositionAsync(int position, Expression<Func<SongPlaylist, bool>>? expression = null);
}
