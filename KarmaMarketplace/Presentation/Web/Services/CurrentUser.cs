using KarmaMarketplace.Application.Common.Interfaces;
using System.Security.Claims;

namespace KarmaMarketplace.Presentation.Web.Services
{
    public class CurrentUser : IUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? Id
        {
            get
            {
                if (_httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false)
                {
                    var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (userIdClaim != null && Guid.TryParse(userIdClaim, out Guid result))
                    {
                        return result;
                    }
                }

                return null;
            }
        }
    }
}
