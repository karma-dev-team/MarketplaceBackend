using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Files.Interfaces;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Market.Enums;
using KarmaMarketplace.Domain.Market.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace KarmaMarketplace.Application.Market.Interactors.Game
{
    public class UpdateGame : BaseUseCase<UpdateGameDto, GameEntity>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAccessPolicy _accessPolicy;
        private readonly IFileService _fileService; 

        public UpdateGame(IApplicationDbContext dbContext, IAccessPolicy accessPolicy, IFileService fileService) {
            _context = dbContext; 
            _accessPolicy = accessPolicy;
            _fileService = fileService; 
        }

        public async Task<GameEntity> Execute(UpdateGameDto dto)
        {
            var game = await _context.Games.FirstOrDefaultAsync(x => x.Id == dto.GameId);

            Guard.Against.Null(game, message: "Game does not exists");

            await _accessPolicy.FailIfNoAccess(dto.ByUserId, Domain.User.Enums.UserRoles.Admin); 

            if (!string.IsNullOrEmpty(dto.Name))
            {
                game.Name = dto.Name; 
            }
            if (!string.IsNullOrEmpty(dto.Description))
            {
                game.Description = dto.Description; 
            }
            if (dto.Tags != null)
            {
                var tags = dto.Tags;
                game.Tags = JsonSerializer.Serialize(tags); 
            }
            if (dto.Logo != null)
            {
                var file = await _fileService.UploadImage().Execute(dto.Logo);

                game.LogoID = file.Id;
                game.LogoImage = file;
            }

            if (dto.Banner != null)
            {
                var file = await _fileService.UploadImage().Execute(dto.Banner);

                game.BannerImage = file;
                game.BannerID = file.Id; 
            }
            if (dto.Type != null)
            {
                bool ok = Enum.TryParse(dto.Type, out GameTypes type); 
                if (!ok)
                {
                    throw new IncorrectAttributes("type", dto.Type); 
                }

                game.Type = type; 
            }

            _context.Update(game);
            await _context.SaveChangesAsync(); 

            return game;
        }
    }
}
