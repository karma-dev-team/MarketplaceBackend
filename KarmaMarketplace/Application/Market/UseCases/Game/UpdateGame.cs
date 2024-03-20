using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Domain.Common;

namespace KarmaMarketplace.Application.Market.Interactors.Game
{
    public class UpdateGame : BaseUseCase<UpdateGameDto, GameEntity>
    {
        public UpdateGame() { }

        public async Task<GameEntity> Execute(UpdateGameDto dto)
        {
            return;
        }
    }
}
