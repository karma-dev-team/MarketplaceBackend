using Microsoft.AspNetCore.Mvc;
using NameApp.Application.User;
using NameApp.Application.User.Dto;
using NameApp.Application.User.Interfaces;
using NameApp.Presentation.Web.Schemas;
using Swashbuckle.AspNetCore.Annotations;

namespace NameApp.Presentation.Web.Controllers
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
                .RegisterInteractor()
                .Execute(dto);
            return UserScheme.FromEntity(result); 
        }
    }
}
