namespace KarmaMarketplace.Infrastructure.Adapters.FileStorage
{
    public class LocalFileStorageAdapter : IFileStorageAdapter
    {
        private readonly string _baseDirectory;

        public LocalFileStorageAdapter(string baseDirectory)
        {
            _baseDirectory = baseDirectory;
        }

        public async Task UploadFileAsync(string path, Stream fileStream)
        {
            var fullPath = Path.Combine(_baseDirectory, path);
            var directory = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using var file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
            await fileStream.CopyToAsync(file);
        }

        public async Task<Stream> DownloadFileAsync(string path)
        {
            var fullPath = Path.Combine(_baseDirectory, path);
            return new FileStream(fullPath, FileMode.Open, FileAccess.Read);
        }

        public Task DeleteFileAsync(string path)
        {
            var fullPath = Path.Combine(_baseDirectory, path);
            File.Delete(fullPath);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<string>> ListFilesAsync(string path)
        {
            var fullPath = Path.Combine(_baseDirectory, path);
            var files = Directory.EnumerateFiles(fullPath);
            return Task.FromResult(files);
        }

        public Task Configure()
        {
            return Task.CompletedTask; 
        }
    }
}
