using KarmaMarketplace.Application.Common.Interfaces;
using System.Security.Claims;

namespace KarmaMarketplace.Presentation.Web.Services
{
    public class CurrentUser : IUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CurrentUser> _logger;

        public CurrentUser(IHttpContextAccessor httpContextAccessor, ILogger<CurrentUser> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
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
                    else
                    {
                        _logger.LogWarning($"User ID claim not found or invalid. userIdClaim: {userIdClaim}");
                    }
                }
                else
                {
                    _logger.LogWarning("User is not authenticated.");
                }

                return null;
            }
        }
    }
}
