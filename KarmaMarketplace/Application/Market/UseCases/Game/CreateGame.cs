using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Domain.Common;

namespace KarmaMarketplace.Application.Market.Interactors.Game
{
    public class CreateGame : BaseUseCase<CreateProductDto, GameEntity>
    {
        public CreateGame() { }

        public async Task<GameEntity> Execute(CreateProductDto dto)
        {
            return;
        }
    }
}
