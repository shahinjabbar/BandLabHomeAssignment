namespace BandLabHomeAssigment.Domain.Repositories;

public interface IBlobStorageRepository
{
    Task<string> UploadFileAsync(Stream fileStream, string fileName);
    Task<string> GetResizedImageUrlAsync(string originalImageUrl);
}
