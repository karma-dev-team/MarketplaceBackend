using KarmaMarketplace.Application.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarmaMarketplace.Presentation.Web.Controllers
{
    [Route("api/stuff")]
    [ApiController]
    public class StuffControllers : ControllerBase
    {
        private IUserService _userService;

        public StuffControllers(IUserService userService) { 
            _userService = userService;
        }

        [HttpPost("/user/{userId}/block")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<bool>> BlockUser(Guid userId)
        {
            await _userService.Update().Execute(new Application.User.Dto.UpdateUserDto { UserId = userId, Blocked = true });
            return Ok(true);
        }
    }
}
