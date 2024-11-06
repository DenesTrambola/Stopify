namespace Stopify.Domain.Contracts.Other;

public interface IAudioStorageService
{
    Uri GetAudioFileUri(string fileName);
    Task<IEnumerable<string>> ListAudioFilesAsync();
}
