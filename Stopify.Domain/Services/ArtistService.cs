using Stopify.Domain.Contracts.Common;
using Stopify.Domain.Contracts.Services;
using Stopify.Domain.DTOs;
using Stopify.Domain.Entities;
using Stopify.Domain.Other;
using Stopify.Exceptions.ValidationExceptions;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Stopify.Domain.Services;

public class ArtistService : IArtistService
{
    private readonly IUnitOfWork _unit;
    private readonly IUserArtistService _userArtistService;
    private readonly IAlbumArtistService _albumArtistService;
    private readonly ISongArtistService _songArtistService;

    public ArtistService(IUnitOfWork unit,
                         IUserArtistService userArtistService,
                         IAlbumArtistService albumArtistService,
                         ISongArtistService songArtistService)
    {
        _unit = unit;
        _userArtistService = userArtistService;
        _albumArtistService = albumArtistService;
        _songArtistService = songArtistService;
    }

    public async Task CreateAsync(Artist entity)
    {
        var artist = await _unit.Artists.GetByNameAsync(entity.Name);
        if (artist is not null)
            throw new EntityAlreadyExistsException(nameof(Artist));

        await _unit.Artists.AddAsync(entity);
        await _unit.SaveChangesAsync();
    }

    public async Task<IEnumerable<Artist>?> GetAllAsync(Expression<Func<Artist, bool>>? expression = null) =>
        await _unit.Artists.GetAllAsync(expression);

    public async Task<IEnumerable<Artist>?> GetAllByAvatarAsync(string avatar, Expression<Func<Artist, bool>>? expression = null) =>
        await _unit.Artists.GetAllByAvatarAsync(avatar, expression);

    public async Task<IEnumerable<Artist>?> GetAllByBgImageAsync(string bgImage, Expression<Func<Artist, bool>>? expression = null) =>
        await _unit.Artists.GetAllByBgImageAsync(bgImage, expression);

    public async Task<IEnumerable<Artist>?> GetAllByBioAsync(string bio, Expression<Func<Artist, bool>>? expression = null) =>
        await _unit.Artists.GetAllByBioAsync(bio, expression);

    public async Task<IEnumerable<Artist>?> GetAllByCountryIdAsync(int countryId, Expression<Func<Artist, bool>>? expression = null) =>
        await _unit.Artists.GetAllByCountryIdAsync(countryId, expression);

    public async Task<IEnumerable<Artist>?> GetAllNotVerifiedAsync(Expression<Func<Artist, bool>>? expression = null) =>
        await _unit.Artists.GetAllNotVerifiedAsync(expression);

    public async Task<IEnumerable<Artist>?> GetAllVerifiedAsync(Expression<Func<Artist, bool>>? expression = null) =>
        await _unit.Artists.GetAllVerifiedAsync(expression);

    public async Task<Artist?> GetByIdAsync(int id, Expression<Func<Artist, bool>>? expression = null) =>
        await _unit.Artists.GetByIdAsync(id, expression);

    public async Task<Artist?> GetFirstByAvatarAsync(string avatar, Expression<Func<Artist, bool>>? expression = null) =>
        await _unit.Artists.GetFirstByAvatarAsync(avatar, expression);

    public async Task<Artist?> GetFirstByBgImageAsync(string bgImage, Expression<Func<Artist, bool>>? expression = null) =>
        await _unit.Artists.GetFirstByBgImageAsync(bgImage, expression);

    public async Task<Artist?> GetFirstByBioAsync(string bio, Expression<Func<Artist, bool>>? expression = null) =>
        await _unit.Artists.GetFirstByBioAsync(bio, expression);

    public async Task<Artist?> GetFirstByCountryIdAsync(int countryId, Expression<Func<Artist, bool>>? expression = null) =>
        await _unit.Artists.GetFirstByCountryIdAsync(countryId, expression);

    public async Task<Artist?> GetByNameAsync(string name, Expression<Func<Artist, bool>>? expression = null) =>
        await _unit.Artists.GetByNameAsync(name, expression);

    public async Task<Artist?> GetFirstNotVerifiedAsync(Expression<Func<Artist, bool>>? expression = null) =>
        await _unit.Artists.GetFirstNotVerifiedAsync(expression);

    public async Task<Artist?> GetFirstVerifiedAsync(Expression<Func<Artist, bool>>? expression = null) =>
        await _unit.Artists.GetFirstVerifiedAsync(expression);

    public async Task RemoveAsync(ArtistDto dto)
    {
        var artist = await _unit.Artists.GetByNameAsync(dto.Name);
        if (artist is null)
            throw new EntityNotFoundException(nameof(Artist));

        _unit.Artists.Remove(artist);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateAvatarAsync(string name, [StringLength(2048, ErrorMessage = "Maximum length is 2048!")] string? newAvatar)
    {
        var artist = await GetByNameAsync(name);
        if (artist is null)
            throw new EntityNotFoundException(nameof(Artist));

        if (newAvatar is not null)
            newAvatar = UrlValidation.CheckFormat(newAvatar) ? newAvatar : $"{Artist.MainAvatarPath}{newAvatar}.jpg";

        if (artist.Avatar == newAvatar)
            throw new SamePropertyNameException(nameof(Artist) + " " + nameof(Artist.Avatar));
        artist.Avatar = newAvatar;

        _unit.Artists.Update(artist, e => e.Avatar);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateBgImageAsync(string name, [StringLength(2048, ErrorMessage = "Maximum length is 2048!")] string? newBgImage)
    {
        var artist = await GetByNameAsync(name);
        if (artist is null)
            throw new EntityNotFoundException(nameof(Artist));

        newBgImage = UrlValidation.CheckFormat(newBgImage) ? newBgImage :
            newBgImage is null ? null : $"{Artist.MainBgImagePath}{newBgImage}.png";

        if (artist.BgImage == newBgImage)
            throw new SamePropertyNameException(nameof(Artist) + " " + nameof(Artist.BgImage));
        artist.BgImage = newBgImage;

        _unit.Artists.Update(artist, e => e.BgImage);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateBioAsync(string name, [StringLength(200, ErrorMessage = "Maximum length is 200!")] string newBio)
    {
        var artist = await GetByNameAsync(name);
        if (artist is null)
            throw new EntityNotFoundException(nameof(Artist));

        if (artist.Bio == newBio)
            throw new SamePropertyNameException(nameof(Artist.Bio));
        artist.Bio = newBio;

        _unit.Artists.Update(artist, e => e.Bio);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateCountryAsync(string name, [StringLength(60, ErrorMessage = "Maximum length is 60!")] string? newCountry)
    {
        var artist = await GetByNameAsync(name);
        if (artist is null)
            throw new EntityNotFoundException(nameof(Artist));

        if (newCountry is null)
            artist.CountryId = null;
        else
        {
            var country = await _unit.Countries.GetByNameAsync(newCountry);
            if (artist.CountryId == country?.Id)
                throw new SamePropertyNameException(nameof(Artist.Avatar));
            artist.CountryId = country?.Id;
        }

        _unit.Artists.Update(artist, e => e.CountryId);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateNameAsync(string name, [StringLength(50, ErrorMessage = "Maximum length is 50!")] string newName)
    {
        var artist = await GetByNameAsync(name);
        if (artist is null)
            throw new EntityNotFoundException(nameof(Artist));

        if (artist.Name == newName)
            throw new SamePropertyNameException(nameof(Artist.Name));

        var artistByNewName = await _unit.Artists.GetByNameAsync(newName);
        if (artistByNewName is not null)
            throw new EntityAlreadyExistsException(nameof(Artist));

        artist.Name = newName;

        _unit.Artists.Update(artist, e => e.Name);
        await _unit.SaveChangesAsync();
    }

    public async Task UpdateVerifiedAsync(string name, bool isVerified)
    {
        var artist = await GetByNameAsync(name);
        if (artist is null)
            throw new EntityNotFoundException(nameof(Artist));

        if (artist.IsVerified == isVerified)
            throw new SamePropertyNameException(nameof(Artist.IsVerified));
        artist.IsVerified = isVerified;

        _unit.Artists.Update(artist, e => e.IsVerified);
        await _unit.SaveChangesAsync();
    }

    public async Task AddUserAsync(string artistName, string username) =>
        await _userArtistService.CreateAsync(username, artistName);

    public async Task AddAlbumAsync(string artistName, string albumTitle) =>
        await _albumArtistService.CreateAsync(albumTitle, artistName);

    public async Task AddSongAsync(string artistName, string songTitle) =>
        await _songArtistService.CreateAsync(songTitle, artistName);
}
