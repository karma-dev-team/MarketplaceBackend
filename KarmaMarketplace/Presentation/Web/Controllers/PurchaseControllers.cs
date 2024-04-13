using KarmaMarketplace.Application.Payment.Dto;
using KarmaMarketplace.Application.Payment.Interfaces;
using KarmaMarketplace.Domain.Payment.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarmaMarketplace.Presentation.Web.Controllers
{
    [Route("api/purchase/")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PurchaseControllers : ControllerBase
    {
        private IPurchaseService _purchaseService; 

        public PurchaseControllers(IPurchaseService purchaseService) {
            _purchaseService = purchaseService; 
        }

        [HttpGet("me")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ICollection<PurchaseEntity>>> GetMyPurchases([FromBody] GetPurchasesListDto model)
        {
            return Ok(await _purchaseService.GetPurchasesList().Execute(model)); 
        }

        [HttpPost("product/{productId}/buy")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<PurchaseEntity>> CreatePurchase([FromBody] CreatePurchaseDto model) 
        {
            return Ok(await _purchaseService.CreatePurchase().Execute(model)); 
        }

        [HttpPost("{purchaseId}/confirm")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<PurchaseEntity>> ConfirmPurchase([FromBody] ConfirmPurchaseDto model)
        {
            return Ok(await _purchaseService.ConfirmPurchase().Execute(model));
        }

        [HttpGet("user/{userId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ICollection<PurchaseEntity>>> GetUserPurchases(
            [FromBody] GetPurchasesListDto model, 
            Guid userId)
        {
            return Ok(await _purchaseService.GetPurchasesList().Execute(model)); 
        }

        [HttpPatch("{purchaseId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<PurchaseEntity>> UpdatePurchase([FromBody] UpdatePurchaseDto model)
        {
            return Ok(await _purchaseService.UpdatePurchase().Execute(model));
        }
    }
}
