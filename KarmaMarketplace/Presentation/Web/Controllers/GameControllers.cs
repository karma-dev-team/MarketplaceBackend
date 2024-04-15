using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Application.Market.Interfaces;
using KarmaMarketplace.Domain.Market.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarmaMarketplace.Presentation.Web.Controllers
{
    [Route("api/game/")]
    [ApiController]
    public class GameControllers : ControllerBase
    {
        private IGameService _gameService;

        public GameControllers(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("", Name = "create-game")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<GameEntity>> CreateGame([FromBody] CreateGameDto model)
        {
            return Ok(await _gameService.CreateGame().Execute(model));
        }

        [HttpGet("")]
        public async Task<ActionResult<ICollection<GameEntity>>> GetGamesList(
            [FromQuery] GetGamesListDto dto)
        {
            return Ok(await _gameService.GetGamesList().Execute(dto));
        }

        [HttpGet("{gameId}")]
        public async Task<ActionResult<GameEntity>> GetGame(Guid gameId)
        {
            return Ok(await _gameService
                .GetGame()
                .Execute(new GetGameDto() { GameId = gameId }));
        }

        [HttpDelete("{gameId}")]
        public async Task<ActionResult<GameEntity>> DeleteGame(Guid gameId)
        {
            return Ok(await _gameService
                .DeleteGame()
                .Execute(new DeleteGameDto() { GameId = gameId }));
        }

        [HttpPatch("{gameId}")]
        public async Task<ActionResult<GameEntity>> UpdateGame(Guid gameId, [FromBody] UpdateGameDto model)
        {
            return Ok(await _gameService.UpdateGame().Execute(model));
        }
    }
}
