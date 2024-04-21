using Microsoft.AspNetCore.Mvc;
using KarmaMarketplace.Application.User.Dto;
using KarmaMarketplace.Application.User.Interfaces;
using KarmaMarketplace.Presentation.Web.Schemas;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Application.Common.Interfaces;

namespace KarmaMarketplace.Presentation.Web.Controllers
{
    [SwaggerTag("auth")]
    [Route("api/user/")]
    [ApiController]
    public class UserControllers : ControllerBase
    {
        public IUserService UserService;
        private IUser _user; 

        public UserControllers(IUserService userService, IUser user)
        {
            UserService = userService;
            _user = user; 
        }

        [HttpPatch("{userId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<UserScheme>> UpdateUser(Guid userId, [FromBody] UpdateUserDto model)
        {
            var result = await UserService
                .Update()
                .Execute(model);
            return UserScheme.FromEntity(result); 
        }

        [HttpPatch("me")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<UserScheme>> UpdateMe([FromBody] UpdateUserDto model)
        {
            var result = await UserService
                .Update()
                .Execute(model);
            return UserScheme.FromEntity(result);
        }

        [HttpGet("me")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<UserEntity>> GetMe()
        {
            var result = await UserService
                .Get().Execute(new GetUserDto() { UserId = _user.Id });

            return result; 
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserEntity>> GetUserById(Guid userId)
        {
            var result = await UserService
                .Get().Execute(new GetUserDto() { UserId = userId });

            return result;
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult<UserScheme>> DeleteUserById(Guid userId)
        {
            var result = await UserService
                .Delete()
                .Execute(new DeleteUserDto() { UserId = userId });

            return Ok(result); 
        }
    }
}
