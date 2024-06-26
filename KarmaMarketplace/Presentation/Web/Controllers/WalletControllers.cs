﻿using KarmaMarketplace.Application.Payment.Dto;
using KarmaMarketplace.Application.Payment.Interfaces;
using KarmaMarketplace.Domain.Payment.Entities;
using KarmaMarketplace.Domain.Payment.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarmaMarketplace.Presentation.Web.Controllers
{
    [Route("api/wallet/")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class WalletControllers : ControllerBase
    {
        private IWalletService _walletService; 

        public WalletControllers(IWalletService walletService)
        {
            _walletService = walletService; 
        }

        [HttpGet("user/{userId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<WalletEntity>> GetWalletByUserId(Guid userId)
        {
            return Ok(await _walletService
                .GetWallet()
                .Execute(
                    new GetWalletDto() { UserId = userId }
                )
            ); 
        }

        [HttpPost("{walletId}/block")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<bool>> BlockWallet(Guid walletId, [FromBody] string reason)
        {
            return Ok(await _walletService
                .BlockWallet()
                .Execute(
                    new BlockWalletDto() { 
                        Reason = reason, WalletId = walletId })); 
        }

        [HttpPost("{walletId}/balance")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<bool>> BalanceOperation(Guid walletId, [FromBody] Money balance)
        {
            return Ok(await _walletService
                .BalanceOperation()
                .Execute(new BalanceOperationDto() { WalletId = walletId, Balance = balance })); 
        }
    }
}
