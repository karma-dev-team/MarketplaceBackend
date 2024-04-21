using Minio;
using Minio.DataModel;
using Minio.DataModel.Args;
using System.Reactive.Linq;
using System.Security.AccessControl;

namespace KarmaMarketplace.Infrastructure.Adapters.FileStorage
{
    public class MinioStorage : IFileStorageAdapter
    {
        private readonly IMinioClient _minioClient;
        private readonly string _bucketName;

        public MinioStorage(string endpoint, string accessKey, string secretKey, string bucketName)
        {
            _minioClient = new MinioClient()
                .WithCredentials(accessKey, secretKey)
                .WithEndpoint(endpoint)
                .Build();
            _bucketName = bucketName;
        }

        public async Task Configure()
        {
            var existsArgs = new BucketExistsArgs().WithBucket(_bucketName);
            var exists = await _minioClient.BucketExistsAsync(existsArgs);
            if (!exists)
            {
                throw new FileStorageException($"Bucket {_bucketName} does not exists in minio storage"); 
            }
        }

        public async Task UploadFileAsync(string path, Stream fileStream)
        {
            try
            {
                var putObjectArgs = new PutObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(path)
                    .WithStreamData(fileStream); 
                await _minioClient.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new FileStorageException($"Failed to upload file to Minio: {ex.Message}", ex);
            }
        }

        public async Task<Stream> DownloadFileAsync(string path)
        {
            try
            {
                var stream = new MemoryStream();
                var query = new GetObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(path); 
                    
                await _minioClient.GetObjectAsync(query).ConfigureAwait(false);
                stream.Position = 0;
                return stream;
            }
            catch (Exception ex)
            {
                throw new FileStorageException($"Failed to download file from Minio: {ex.Message}", ex);
            }
        }

        public async Task DeleteFileAsync(string path)
        {
            try
            {
                var remove = new RemoveObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(path);
                await _minioClient.RemoveObjectAsync(remove).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new FileStorageException($"Failed to delete file from Minio: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<string>> ListFilesAsync(string path)
        {
            try
            {
                var files = new List<string>();
                var getFiles = new ListObjectsArgs().WithBucket(_bucketName).WithPrefix(path);

                // Obtain an observable sequence of items
                IObservable<Item> objects = _minioClient.ListObjectsAsync(getFiles);

                // Subscribe to the observable sequence and collect items
                IDisposable subscription = objects.Subscribe(
                    item => files.Add(item.Key), // Add each item to the files list
                    ex => { throw new FileStorageException($"Failed to list files in Minio: {ex.Message}", ex); }, // Handle errors
                    () => { /* OnCompleted handler if needed */ });

                // Wait for the subscription to complete asynchronously
                await Task.Delay(0); // This delay ensures that the method is asynchronous

                // Dispose the subscription (cleanup)
                subscription.Dispose();

                return files;
            }
            catch (Exception ex)
            {
                throw new FileStorageException($"Failed to list files in Minio: {ex.Message}", ex);
            }
        }
    }
}
