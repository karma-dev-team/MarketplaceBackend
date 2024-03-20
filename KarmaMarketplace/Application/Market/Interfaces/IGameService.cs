using KarmaMarketplace.Application.Market.Interactors.Game;

namespace KarmaMarketplace.Application.Market.Interfaces
{
    public interface IGameService
    {
        public CreateGame CreateGame();
        public UpdateGame UpdateGame();
        public DeleteGame DeleteGame();
        public GetGame GetGame();
        public GetGamesList GetCategoriesList();
        public CountGames CountGames(); 
    }
}
