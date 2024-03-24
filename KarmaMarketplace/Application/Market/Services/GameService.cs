using KarmaMarketplace.Application.Market.Interactors.Game;
using KarmaMarketplace.Application.Market.Interfaces;

namespace KarmaMarketplace.Application.Market.Services
{
    public class GameService : IGameService
    {
        public CreateGame CreateGame();
        public UpdateGame UpdateGame();
        public DeleteGame DeleteGame();
        public GetGame GetGame();
        public GetGamesList GetCategoriesList();
        public CountGames CountGames();
    }
}
