using KarmaMarketplace.Application.Payment.Dto;
using KarmaMarketplace.Application.Payment.Interfaces;
using KarmaMarketplace.Domain.Payment.Entities;
using KarmaMarketplace.Infrastructure.Adapters.Payment;
using KarmaMarketplace.Infrastructure.Adapters.Payment.Systems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarmaMarketplace.Presentation.Web.Controllers
{
    [Route("api/transaction/")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TransactionControllers : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IWalletService _walletService; 

        public TransactionControllers(ITransactionService transactionService, IWalletService walletService)
        {
            _walletService = walletService;
            _transactionService = transactionService;
        }

        [HttpGet("user/{userId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ICollection<TransactionEntity>>> GetUserTransactions(
            Guid userId, [FromQuery] GetTransactionsListFilterDto filter)
        {
            var wallet = await _walletService
                .GetWallet()
                .Execute(new GetWalletDto() { UserId = userId });

            return Ok(
                await _transactionService
                    .GetTransactionsList()
                    .Execute(new GetTransactionsListDto() { 
                        WalletId = wallet.Id, 
                        Operation = filter.Operation, 
                        Ends = filter.Ends, 
                        FromDate = filter.FromDate, 
                        Start = filter.Start, 
                        ToDate = filter.ToDate, 
                        TransactionProvider = filter.TransactionProvider
                    })); 
        }

        [HttpPost("{txId}/complete")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<TransactionEntity>> ForcefullyCompleteTransaction(
            Guid txId, [FromBody] UpdateTransactionDto dto)
        {
            return Ok(
                await _transactionService
                    .UpdateTransaction()
                    .Execute(dto)); 
        }

        [HttpPost("{txId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<TransactionEntity>> HandleTransaction(Guid txId, [FromBody] HandleTransactionDto dto)
        {
            return Ok(
                await _transactionService
                    .HandleTransaction()
                    .Execute(dto));
        }

        [HttpPost("/payout/paypalych")]
        public async Task<ActionResult<bool>> HandlePaypalychResult([FromBody] GatewayResultDto model)
        {
            return Ok(await _transactionService
                .HandleGatewayResult()
                .Execute(model)); 
        }

        [HttpGet("/providers")]
        public async Task<ActionResult<ICollection<TransactionProviderEntity>>> GetProviders()
        {
            return Ok(
                _transactionService
                    .GetTransactionProviders()
                    .Execute(new GetTransactionProvidersDto() { })); 
        }
    }
}
