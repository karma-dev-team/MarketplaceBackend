using KarmaMarketplace.Application.Common.Interactors;

namespace KarmaMarketplace.Application.Market.Interactors.Game
{
    public class GetGame : BaseUseCase<GetGameDto, GameEntity>
    {
        public GetGame() { }

        public async Task<GameEntity> Execute(GetGameDto dto)
        {
            return;
        }
    }
}
