using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Domain.Market.Entities;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Market.Interactors.Game
{
    public class UpdateGame : BaseUseCase<UpdateGameDto, GameEntity>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAccessPolicy _accessPolicy; 

        public UpdateGame(IApplicationDbContext dbContext, IAccessPolicy accessPolicy) {
            _context = dbContext; 
            _accessPolicy = accessPolicy;
        }

        public async Task<GameEntity> Execute(UpdateGameDto dto)
        {
            var game = await _context.Games.FirstOrDefaultAsync(x => x.Id == dto.GameId);

            Guard.Against.Null(game, message: "Game does not exists"); 

            if (!string.IsNullOrEmpty(dto.Name))
            {

            }
            if (!string.IsNullOrEmpty(dto.Description))
            {

            }
            if (dto.Tags != null)
            {

            }
            if (dto.Banner != null)
            {

            }
            if (dto.Type != null)
            {

            }

            return new();
        }
    }
}
