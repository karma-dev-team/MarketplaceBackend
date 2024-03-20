using Microsoft.AspNetCore.Mvc;
using KarmaMarketplace.Application.User;
using KarmaMarketplace.Application.User.Dto;
using KarmaMarketplace.Application.User.Interfaces;
using KarmaMarketplace.Presentation.Web.Schemas;
using Swashbuckle.AspNetCore.Annotations;

namespace KarmaMarketplace.Presentation.Web.Controllers
{
    [SwaggerTag("auth")]
    [Route("api/user/")]
    [ApiController]
    public class UserControllers
    {
        public IUserService UserService;

        public UserControllers(IUserService userService)
        {
            UserService = userService; 
        }

        [HttpPost("register")]
        public async Task<UserScheme> Register()
        {
            CreateUserDto dto = new();
            var result = await UserService
                .Register()
                .Execute(dto);
            return UserScheme.FromEntity(result); 
        }
    }
}
