using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Files.Dto;
using KarmaMarketplace.Domain.Files.Entities;
using KarmaMarketplace.Infrastructure.Adapters.FileStorage;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Files.UseCases
{
    public class UploadFile : BaseUseCase<CreateFileDto, FileEntity>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorageAdapter _fileStorage;
        private readonly HttpClient _httpClient;

        public UploadFile(IApplicationDbContext dbContext, IFileStorageAdapter fileStorage, IHttpClientFactory httpClient)
        {
            _context = dbContext;
            _fileStorage = fileStorage;
            _httpClient = httpClient.CreateClient(); 
        }

        public async Task<FileEntity> Execute(CreateFileDto dto)
        {
            if (dto.FileId != null)
            {
                var image = await _context.Files
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == dto.FileId);
                Guard.Against.Null(image, message: $"Image does not exists with id: {dto.FileId}");

                return image; 
            }
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

            string name; 
            if (dto.Name != null)
            {
                name = dto.Name;
            } else
            {
                name = Guid.NewGuid().ToString(); 
            }

            var uniqueFileName = GenerateUniqueFileName(name);
            var filePath = $"files/{uniqueFileName}"; // Пример пути для сохранения файла

            // Сохраняем файл во внешнем хранилище
            await _fileStorage.UploadFileAsync(filePath, fileStream);

            // Создаем новую сущность ImageEntity для сохранения информации о загруженном файле
            var fileEntity = new FileEntity(Guid.NewGuid(), name, filePath, dto.MimeType, 0); // Размер может быть установлен, если доступен

            // Сохраняем сущность в базе данных
            _context.Files.Add(fileEntity);
            await _context.SaveChangesAsync();

            // Возвращаем сущность для дальнейшего использования
            return fileEntity;
        }

        private string GenerateUniqueFileName(string? originalFileName)
        {
            // Пример простой реализации. В реальных сценариях может потребоваться более сложная логика для генерации имени.
            var extension = Path.GetExtension(originalFileName);
            return Guid.NewGuid().ToString() + extension;
        }
    }
}
