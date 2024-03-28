using KarmaMarketplace.Application.Market.Interactors.Game;
using KarmaMarketplace.Application.Market.UseCases.Game;

namespace KarmaMarketplace.Application.Market.Interfaces
{
    public interface IGameService
    {
        public CreateGame CreateGame();
        public UpdateGame UpdateGame();
        public DeleteGame DeleteGame();
        public GetGame GetGame();
        public GetGamesList GetGamesList();
        public CountGames CountGames(); 
    }
}
