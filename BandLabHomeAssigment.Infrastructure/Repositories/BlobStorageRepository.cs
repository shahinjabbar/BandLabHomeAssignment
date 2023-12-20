using Aspose.Imaging;
using Azure.Storage.Blobs;
using BandLabHomeAssigment.Domain.Repositories;
using System.Drawing;

namespace BandLabHomeAssigment.Infrastructure.Repositories;
public class BlobStorageRepository(BlobServiceClient blobService) : IBlobStorageRepository
{
    private readonly BlobServiceClient _blobService = blobService;

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
    {
        var blobContainerClient = _blobService.GetBlobContainerClient("myblobcontainer");

        var blobClient = blobContainerClient.GetBlobClient(fileName);

        try
        {
            await blobClient.UploadAsync(fileStream, overwrite: true);
            return blobClient.Uri.AbsoluteUri;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<string> GetResizedImageUrlAsync(string originalImageUrl)
    {
        try
        {
            var parsedUri = new Uri(originalImageUrl);
            var containerName = parsedUri.Segments[1];
            var blobName = parsedUri.Segments[2];

            var blobContainerClient = _blobService.GetBlobContainerClient(containerName);

            var blobClient = blobContainerClient.GetBlobClient(blobName);

            using var memoryStream = new MemoryStream();
            await blobClient.DownloadToAsync(memoryStream);
            memoryStream.Position = 0;
            
            using var image = Image.Load(memoryStream);
            image.Resize(600, 600);
            memoryStream.Position = 0;
            memoryStream.SetLength(0);
            image.Save(memoryStream);
            memoryStream.Position = 0;
            var resizedBlobName = Path.ChangeExtension(originalImageUrl, "_resized.jpg");
            var resizedBlobClient = blobContainerClient.GetBlobClient(resizedBlobName);
            await resizedBlobClient.UploadAsync(memoryStream, overwrite: false);
            // does not work right now
            return resizedBlobClient.Uri.AbsoluteUri;
        }
        catch (Exception)
        {
            throw;
        }
    }
}