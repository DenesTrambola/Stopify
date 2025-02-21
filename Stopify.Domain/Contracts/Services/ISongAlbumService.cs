namespace Stopify.Domain.Contracts.Services;

public interface ISongAlbumService
{
    Task UpdateAsync(string songTitle, string? albumTitle, int? position);
}
