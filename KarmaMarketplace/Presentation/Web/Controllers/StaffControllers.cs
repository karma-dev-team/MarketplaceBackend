using KarmaMarketplace.Application.Staff.Interfaces;
using KarmaMarketplace.Application.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarmaMarketplace.Presentation.Web.Controllers
{
    [Route("api/stuff")]
    [ApiController]
    public class StaffControllers : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IStaffService _staffService; 

        public StaffControllers(IUserService userService, IStaffService staffService) { 
            _userService = userService;
            _staffService = staffService;
        }

        [HttpPost("/user/{userId}/block")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<bool>> BlockUser(Guid userId)
        {
            await _userService.Update().Execute(new Application.User.Dto.UpdateUserDto { UserId = userId, Blocked = true });
            return Ok(true);
        }

        [HttpPost("/user/{userId}/warn")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<bool>> WarnUser(Guid userId)
        {
            await _userService.WarnUser().Execute(userId); 
            return Ok(true);
        }
    }
}
