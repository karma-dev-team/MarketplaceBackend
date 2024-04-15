using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Files.Interfaces;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Market.Enums;
using KarmaMarketplace.Domain.Market.Exceptions;
using System.Text.Json;

namespace KarmaMarketplace.Application.Market.Interactors.Game
{
    public class CreateGame : BaseUseCase<CreateGameDto, GameEntity>
    {
        public IApplicationDbContext _context;
        public IFileService _fileService; 

        public CreateGame(IApplicationDbContext dbContext, IFileService fileService) {
            _context = dbContext; 
            _fileService = fileService;
        }

        public async Task<GameEntity> Execute(CreateGameDto dto)
        {
            var logo = await _fileService.UploadFile().Execute(dto.Logo);
            var banner = await _fileService.UploadFile().Execute(dto.Banner);

            var game = GameEntity.Create(
                name: dto.Name,
                description: dto.Description,
                type: dto.Type,
                tags: JsonSerializer.Serialize(dto.Tags), 
                banner: banner, 
                logo: logo); 

            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            return game;
        }
    }
}
