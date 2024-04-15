using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Market.Enums;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Market.UseCases.Game
{
    public class CountGames : BaseUseCase<GameTypes?, int>
    {
        private IApplicationDbContext _context; 

        public CountGames(IApplicationDbContext dbContext) {
            _context = dbContext; 
        }

        public async Task<int> Execute(GameTypes? type)
        {
            return await _context.Games
                .Where(x => x.Type == type)
                .CountAsync(); 
        }
    }
}
