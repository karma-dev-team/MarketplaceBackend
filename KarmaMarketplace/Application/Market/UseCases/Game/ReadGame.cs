using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Domain.Market.Entities;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KarmaMarketplace.Application.Market.Interactors.Game
{
    public class GetGame : BaseUseCase<GetGameDto, GameEntity>
    {
        public GetGame() {
        
        }

        public async Task<GameEntity> Execute(GetGameDto dto)
        {
            return;
        }
    }

    public class GetGamesList : BaseUseCase<GetGamesListDto, ICollection<GameEntity>>
    {
        public GetGamesList()
        {

        }

        public async Task<ICollection<GameEntity>> Execute(GetGamesListDto dto)
        {

        }
    }
}
