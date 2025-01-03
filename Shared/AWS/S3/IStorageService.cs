namespace Shared.AWS.S3
{
    public interface IStorageService
    {
        Task<string> UploadFileAsync(string bucketName, string key, Stream fileStream);
        Task<Stream?> DownloadFileAsync(string bucketName, string key);
        Task DeleteFileAsync(string bucketName, string key);
    }
}
