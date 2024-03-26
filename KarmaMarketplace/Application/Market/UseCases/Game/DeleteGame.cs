using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Market.Events;
using KarmaMarketplace.Infrastructure.EventDispatcher;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Market.Interactors.Game
{
    public class DeleteGame : BaseUseCase<DeleteGameDto, GameEntity>
    {
        public IApplicationDbContext _context;
        public IAccessPolicy _accessPolicy;
        public IEventDispatcher _eventDispatcher; 

        public DeleteGame(
            IApplicationDbContext dbContext,
            IAccessPolicy accessPolicy, 
            IEventDispatcher eventDispatcher) 
        {
            _context = dbContext; 
            _accessPolicy = accessPolicy;
            _eventDispatcher = eventDispatcher; 
        }

        public async Task<GameEntity> Execute(DeleteGameDto dto)
        {
            await _accessPolicy.FailIfNoAccess(Domain.User.Enums.UserRoles.Admin); 

            var game = await _context.Games.FirstOrDefaultAsync(x => x.Id == dto.GameId);

            Guard.Against.Null(game, message: "Game does not exists");

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            _eventDispatcher.Dispatch(new GameDeleted(game, DateTime.UtcNow));

            return game;
        }
    }
}
