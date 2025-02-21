using Azure.Storage.Blobs;
using Stopify.Domain.Contracts.Other;

namespace Stopify.Infrastructure.Other;

public class AzureBlobContext : IAudioStorageContext
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly BlobContainerClient _containerClient;

    public AzureBlobContext(string connectionString, string containerName)
    {
        _blobServiceClient = new BlobServiceClient(connectionString);
        _containerClient = new BlobContainerClient(connectionString, containerName);
    }

    public Uri GetAudioFileUri(string fileName)
    {
        var blobClient = _blobServiceClient.GetBlobContainerClient(_containerClient.Name).GetBlobClient(fileName);
        return blobClient.Uri;
    }

    public async Task<IEnumerable<string>> ListAudioFilesAsync()
    {
        var songs = new List<string>();

        await foreach (var blobItem in _containerClient.GetBlobsAsync())
        {
            var blobClient = _containerClient.GetBlobClient(blobItem.Name);
            songs.Add(blobClient.Uri.ToString());
        }

        return songs;
    }
}
