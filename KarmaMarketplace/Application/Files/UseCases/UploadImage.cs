using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Files.Dto;
using KarmaMarketplace.Domain.Files.Entities;
using KarmaMarketplace.Infrastructure.Adapters.FileStorage;

namespace KarmaMarketplace.Application.Files.UseCases
{
    public class UploadImage : BaseUseCase<CreateFileDto, ImageEntity>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorageAdapter _fileStorage;
        private readonly HttpClient _httpClient;

        public UploadImage(IApplicationDbContext dbContext, IFileStorageAdapter fileStorage, IHttpClientFactory httpClient)
        {
            _context = dbContext;
            _fileStorage = fileStorage;
            _httpClient = httpClient.CreateClient(); 
        }

        public async Task<ImageEntity> Execute(CreateFileDto dto)
        {
            Stream? fileStream = dto.Stream;
            if (fileStream == null && !string.IsNullOrEmpty(dto.DownloadUrl))
            {
                // Если Stream не предоставлен, но есть URL, загружаем файл из интернета
                var response = await _httpClient.GetAsync(dto.DownloadUrl, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                fileStream = await response.Content.ReadAsStreamAsync();
            }

            if (fileStream == null)
            {
                throw new InvalidOperationException("Не удалось получить поток файла.");
            }

            var uniqueFileName = GenerateUniqueFileName(dto.Name);
            var filePath = $"images/{uniqueFileName}"; // Пример пути для сохранения файла

            // Сохраняем файл во внешнем хранилище
            await _fileStorage.UploadFileAsync(filePath, fileStream);

            // Создаем новую сущность ImageEntity для сохранения информации о загруженном файле
            var imageEntity = new ImageEntity(Guid.NewGuid(), dto.Name!, filePath, dto.MimeType, 0); // Размер может быть установлен, если доступен

            // Сохраняем сущность в базе данных
            _context.Images.Add(imageEntity);
            await _context.SaveChangesAsync();

            // Возвращаем сущность для дальнейшего использования
            return imageEntity;
        }

        private string GenerateUniqueFileName(string? originalFileName)
        {
            // Пример простой реализации. В реальных сценариях может потребоваться более сложная логика для генерации имени.
            var extension = Path.GetExtension(originalFileName);
            return Guid.NewGuid().ToString() + extension;
        }
    }
}
