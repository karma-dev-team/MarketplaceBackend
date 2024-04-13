using KarmaMarketplace.Application.Common.Models;
using KarmaMarketplace.Application.Market.Interfaces;
using KarmaMarketplace.Domain.Market.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarmaMarketplace.Presentation.Web.Controllers
{
    [Route("api/review/")]
    [ApiController]
    public class ReviewControllers : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewControllers(IReviewService reviewService) {
            _reviewService = reviewService;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ICollection<ReviewEntity>>> GetReviews(
            Guid userId, [FromQuery] InputPagination pagination)
        {
            return Ok(
                await _reviewService
                    .GetReviewsList()
                    .Execute(new Application.Market.Dto.GetReviewsListDto() { 
                        UserId = userId, 
                        Start = pagination.Start, 
                        Ends = pagination.Ends})); 
        }

        [HttpGet("chat/{chatId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ICollection<ReviewEntity>>> GetChatReviews(
            Guid chatId, [FromQuery] InputPagination pagination)
        {
            return Ok(
                await _reviewService
                    .GetReviewsList()
                    .Execute(new Application.Market.Dto.GetReviewsListDto() { 
                        UserId = chatId,
                        Start = pagination.Start,
                        Ends = pagination.Ends
                    }));
        }
    }
}
