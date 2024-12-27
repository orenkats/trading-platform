using Amazon.S3;
using Amazon.S3.Model;


namespace Shared.AWS.S3
{
    public class S3StorageService : IStorageService
    {
        private readonly IAmazonS3 _s3Client;

        public S3StorageService(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public async Task<string> UploadFileAsync(string bucketName, string key, Stream fileStream)
        {
            var request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = key,
                InputStream = fileStream
            };

            await _s3Client.PutObjectAsync(request);

            // Returning the key as an identifier
            return key;
        }

        public async Task<Stream?> DownloadFileAsync(string bucketName, string key)
        {
            var request = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = key
            };

            try
            {
                var response = await _s3Client.GetObjectAsync(request);
                return response.ResponseStream;
            }
            catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task DeleteFileAsync(string bucketName, string key)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = key
            };

            await _s3Client.DeleteObjectAsync(request);
        }
    }
}
