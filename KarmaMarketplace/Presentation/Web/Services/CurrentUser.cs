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
                bool ok = Guid.TryParse(
                    _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out Guid result);
                if (ok)
                {
                    return result;
                }
                else { return null; }
            }
        }
    }
}
