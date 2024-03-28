using Microsoft.AspNetCore.Mvc;
using KarmaMarketplace.Application.User;
using KarmaMarketplace.Application.User.Dto;
using KarmaMarketplace.Application.User.Interfaces;
using KarmaMarketplace.Presentation.Web.Schemas;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using KarmaMarketplace.Domain.User.Entities;
using Telegram.Bot.Types;
using KarmaMarketplace.Presentation.Web.Services;
using KarmaMarketplace.Application.Common.Interfaces;

namespace KarmaMarketplace.Presentation.Web.Controllers
{
    [SwaggerTag("auth")]
    [Route("api/user/")]
    [ApiController]
    public class UserControllers
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
        public async Task<UserScheme> UpdateUser(Guid id, [FromBody] UpdateUserDto model)
        {
            var result = await UserService
                .Update()
                .Execute(model);
            return UserScheme.FromEntity(result); 
        }

        [HttpPatch("/me")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<UserScheme> UpdateMe([FromBody] UpdateUserDto model)
        {
            var result = await UserService
                .Update()
                .Execute(model);
            return UserScheme.FromEntity(result);
        }

        [HttpGet("/me")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<UserEntity> GetMe()
        {
            var result = await UserService
                .Get().Execute(new GetUserDto() { UserId = _user.Id });

            return result; 
        }

        [HttpGet("/{userId}")]
        public async Task<UserEntity> GetUserById(Guid userId)
        {
            var result = await UserService
                .Get().Execute(new GetUserDto() { UserId = userId });

            return result;
        }

        [HttpDelete("/{userId}")]
        public async Task<UserScheme> DeleteUserById(Guid userId)
        {

        }
    }
}
