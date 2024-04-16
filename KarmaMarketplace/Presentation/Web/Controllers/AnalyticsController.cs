using KarmaMarketplace.Application.Market.Dto;
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
        private IProductService _productService;

        public AnalyticsController(IReviewService reviewService, IProductService productService) { 
            _reviewService = reviewService;
            _productService = productService;
        }

        [HttpGet("user/{userId}/analytics")]
        public async Task<ActionResult<UserAnalyticsSchema>> AvarageRating(Guid userId)
        {
            var reviews = await _reviewService
                .GetReviewsList()
                .Execute(new GetReviewsListDto() { UserId = userId });
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

        [HttpGet("product/{tempProductId}/analytics")]
        public async Task<ActionResult<AnalyticsInformationDto>> GetAnalytics(
            [FromQuery] GetAnalyticsDto model, Guid tempProductId)
        {
            return Ok(await _productService.GetAnalyticsInformation().Execute(model)); 
        }
    }
}
