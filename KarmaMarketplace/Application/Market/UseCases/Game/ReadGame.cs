using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Market.Interactors.Game
{
    public class GetGame : BaseUseCase<GetGameDto, GameEntity>
    {
        private IApplicationDbContext _context;

        public GetGame(IApplicationDbContext dbContext) {
            _context = dbContext;
        }

        public async Task<GameEntity> Execute(GetGameDto dto)
        {
            GameEntity? game;
            if (dto.GameId != null)
            {
                game = await _context.Games
                    .IncludeStandard()
                    .FirstOrDefaultAsync(x => x.Id == dto.GameId);
            } else
            {
                game = await _context.Games
                    .IncludeStandard()
                    .FirstOrDefaultAsync(x => x.Name == dto.Name);
            }
            Guard.Against.Null(game, message: $"game does not exists, id: {dto.GameId}"); 

            return game;
        }
    }

    public class GetGamesList : BaseUseCase<GetGamesListDto, ICollection<GameEntity>>
    {
        private IApplicationDbContext _context;

        public GetGamesList(IApplicationDbContext dbContext)
        {
            _context = dbContext; 
        }

        public async Task<ICollection<GameEntity>> Execute(GetGamesListDto dto)
        {
            var query = _context.Games
                .IncludeStandard()
                .AsQueryable(); 

            if (!string.IsNullOrEmpty(dto.Name))
            {
                query = query.Where(x =>
                    EF.Functions
                        .ToTsVector(x.Name)
                        .Matches(EF.Functions.ToTsQuery(dto.Name))
                    ); 
            }
            if (dto.Type != null)
            {
                query = query.Where(x => x.Type == dto.Type); 
            }

            return await query.ToListAsync(); 
        }
    }
}
