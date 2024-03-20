using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Domain.Common;

namespace KarmaMarketplace.Application.Market.Interactors.Game
{
    public class DeleteGame : BaseUseCase<Guid, GameEntity>
    {
        public DeleteGame() { }

        public async Task<GameEntity> Execute(Guid dto)
        {
            return;
        }
    }
}
