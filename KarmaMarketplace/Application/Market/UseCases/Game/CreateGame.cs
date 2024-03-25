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
            var ok = Enum.TryParse(dto.Type, out GameTypes type);
            if (!ok)
            {
                throw new IncorrectAttributes("type", dto.Type); 
            }

            var logo = await _fileService.UploadImage().Execute(dto.Logo);
            var banner = await _fileService.UploadImage().Execute(dto.Banner);

            var game = GameEntity.Create(
                name: dto.Name,
                description: dto.Description,
                type: type,
                tags: JsonSerializer.Serialize(dto.Tags), 
                banner: banner, 
                logo: logo); 

            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            return game;
        }
    }
}
