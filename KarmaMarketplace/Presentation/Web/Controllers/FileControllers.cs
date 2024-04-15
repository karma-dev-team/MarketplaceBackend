using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Files.Interfaces;
using KarmaMarketplace.Domain.Files.Entities;
using KarmaMarketplace.Infrastructure.Adapters.FileStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace KarmaMarketplace.Presentation.Web.Controllers
{
    [Route("api/files/")]
    [ApiController]
    public class FileControllers : ControllerBase
    {
        // Govno kod 
        private readonly IFileService _fileService;
        private readonly IFileStorageAdapter _fileStorage; 
        private readonly IApplicationDbContext _context;    

        public FileControllers(IFileService fileService, IFileStorageAdapter fileStorage, IApplicationDbContext dbContext, IConfiguration configuration) { 
            _fileService = fileService;
            _fileStorage = fileStorage;
            _context = dbContext;
        }

        [HttpPost("upload")]
        public async Task<ActionResult<FileEntity>> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Файл не был загружен.");

            return Ok(await _fileService
                .UploadFile()
                .Execute(new Application.Files.Dto.CreateFileDto() { Stream = file.OpenReadStream() })); 
        }

        [HttpPost("download/{fileId}")]
        public async Task<IActionResult> DownloadFile(Guid fileId)
        {
            var fileModel = await _context.Files.FirstOrDefaultAsync(x => x.Id == fileId);
            if (fileModel == null)
                return NotFound();
            // very ineffective!!!  
            var file = await _fileStorage.DownloadFileAsync(fileModel.FilePath);
            byte[] buffer = []; 

            await file.ReadAsync(buffer); 

            return new FileContentResult(buffer, "application/octet-stream");
        }
    }
}
