namespace Stopify.Domain.Contracts.Other;

public interface IAudioStorageContext
{
    Uri GetAudioFileUri(string fileName);
    Task<IEnumerable<string>> ListAudioFilesAsync();
}
