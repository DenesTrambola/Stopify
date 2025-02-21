using Stopify.Domain.Contracts.Common;
using Stopify.Domain.DTOs;
using Stopify.Domain.Entities;
using System.Linq.Expressions;

namespace Stopify.Domain.Contracts.Services;

public interface IArtistService : IEntityService<Artist, ArtistDto>
{
    Task<Artist?> GetByNameAsync(string name, Expression<Func<Artist, bool>>? expression = null);
    Task<Artist?> GetFirstByBioAsync(string bio, Expression<Func<Artist, bool>>? expression = null);
    Task<Artist?> GetFirstByCountryIdAsync(int countryId, Expression<Func<Artist, bool>>? expression = null);
    Task<Artist?> GetFirstByAvatarAsync(string avatar, Expression<Func<Artist, bool>>? expression = null);
    Task<Artist?> GetFirstByBgImageAsync(string bgImage, Expression<Func<Artist, bool>>? expression = null);
    Task<Artist?> GetFirstVerifiedAsync(Expression<Func<Artist, bool>>? expression = null);
    Task<Artist?> GetFirstNotVerifiedAsync(Expression<Func<Artist, bool>>? expression = null);

    Task<IEnumerable<Artist>?> GetAllByBioAsync(string bio, Expression<Func<Artist, bool>>? expression = null);
    Task<IEnumerable<Artist>?> GetAllByCountryIdAsync(int countryId, Expression<Func<Artist, bool>>? expression = null);
    Task<IEnumerable<Artist>?> GetAllByAvatarAsync(string avatar, Expression<Func<Artist, bool>>? expression = null);
    Task<IEnumerable<Artist>?> GetAllByBgImageAsync(string bgImage, Expression<Func<Artist, bool>>? expression = null);
    Task<IEnumerable<Artist>?> GetAllVerifiedAsync(Expression<Func<Artist, bool>>? expression = null);
    Task<IEnumerable<Artist>?> GetAllNotVerifiedAsync(Expression<Func<Artist, bool>>? expression = null);

    Task UpdateNameAsync(string name, string newName);
    Task UpdateBioAsync(string name, string newBio);
    Task UpdateCountryAsync(string name, string? newCountry);
    Task UpdateAvatarAsync(string name, string? newAvatar);
    Task UpdateBgImageAsync(string name, string? newBgImage);
    Task UpdateVerifiedAsync(string name, bool isVerified);

    Task AddUserAsync(string artistName, string username);
    Task AddAlbumAsync(string artistName, string albumTitle);
    Task AddSongAsync(string artistName, string songTitle);
}
