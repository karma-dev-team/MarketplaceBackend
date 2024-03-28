using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Domain.Market.Entities;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Market.UseCases.Game
{
    public class CountGames : BaseUseCase<Guid?, int>
    {
        private IApplicationDbContext _context; 

        public CountGames(IApplicationDbContext dbContext) {
            _context = dbContext; 
        }

        public async Task<int> Execute(Guid? _)
        {
            return await _context.Games.CountAsync(); 
        }
    }
}
