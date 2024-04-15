using KarmaMarketplace.Application.Market.Interfaces;
using KarmaMarketplace.Presentation.Web.Schemas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarmaMarketplace.Presentation.Web.Controllers
{
    [Route("api/analytics")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private IReviewService _reviewService;

        public AnalyticsController(IReviewService reviewService) { 
            _reviewService = reviewService;
        }

        [HttpGet("user/{userId}/analytics")]
        public async Task<ActionResult<UserAnalyticsSchema>> AvarageRating(Guid userId)
        {
            var reviews = await _reviewService
                .GetReviewsList()
                .Execute(new Application.Market.Dto.GetReviewsListDto() { UserId = userId });
            decimal rating;
            if (reviews.Count == 0)
            {
                rating = 0;
            }
            else
            {
                rating = reviews.Select(reviews => reviews.Rating).Sum();
            } 
            return Ok(new UserAnalyticsSchema() { AvarageRating = rating }); 
        }
    }
}
