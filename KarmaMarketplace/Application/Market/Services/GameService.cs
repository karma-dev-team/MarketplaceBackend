using KarmaMarketplace.Application.Market.Interactors.Game;
using KarmaMarketplace.Application.Market.Interfaces;
using KarmaMarketplace.Application.Market.UseCases.Game;

namespace KarmaMarketplace.Application.Market.Services
{
    public class GameService : IGameService
    {
        private readonly IServiceProvider ServiceProvider;

        public GameService(
            IServiceProvider serviceProvider
        )
        {
            ServiceProvider = serviceProvider;
        }

        public CreateGame CreateGame()
        {
            return ServiceProvider.GetRequiredService<CreateGame>();
        }
        public UpdateGame UpdateGame()
        {
            return ServiceProvider.GetRequiredService<UpdateGame>();
        }
        public DeleteGame DeleteGame()
        {
            return ServiceProvider.GetRequiredService<DeleteGame>();
        }
        public GetGame GetGame()
        {
            return ServiceProvider.GetRequiredService<GetGame>();
        }
        public GetGamesList GetGamesList()
        {
            return ServiceProvider.GetRequiredService<GetGamesList>();
        }
        public CountGames CountGames()
        {
            return ServiceProvider.GetRequiredService<CountGames>();
        }
    }
}
