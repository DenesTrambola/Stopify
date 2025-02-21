using Stopify.Domain.Contracts.Other;

namespace Stopify.Domain.Other;

public class AudioStorageService : IAudioStorageService
{
    private readonly IAudioStorageContext _audioStorageContext;

    public AudioStorageService(IAudioStorageContext audioStorageService) =>
        _audioStorageContext = audioStorageService;

    public Uri GetAudioFileUri(string fileName) =>
        _audioStorageContext.GetAudioFileUri(fileName);

    public Task<IEnumerable<string>> ListAudioFilesAsync() =>
        _audioStorageContext.ListAudioFilesAsync();


}
