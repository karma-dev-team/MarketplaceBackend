using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarmaMarketplace.Presentation.Web.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TransactionControllers : ControllerBase
    {
    }
}
