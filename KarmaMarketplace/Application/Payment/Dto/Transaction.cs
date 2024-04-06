using KarmaMarketplace.Application.Common.Models;
using KarmaMarketplace.Domain.Payment.Enums;
using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Application.Payment.Dto
{
    public class SolveProblemDto
    {
    }

    public class CreateTransactionDto
    {

    }

    public class GetTransactionsListDto : InputPagination
    {
        [Required]
        public Guid WalletId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public TransactionOperations? Operation { get; set; }
        public string? TransactionProvider { get; set; }
    }

    public class UpdateTransactionDto
    {

    }

    public class GetTransactionProvidersDto
    {

    }

    public class HandleTransactionDto
    {
        [Required]
        public Guid TransactionId { get; set; } 
    }

    public class GatewayResultDto
    {
        [Required]
        public bool Success { get; set; } = false;
        [Required]
        public string CustomId { get; set; } = string.Empty;
        [Required]
        public decimal Fee { get; set; }
        [Required]
        public CurrencyEnum CurrencyIn { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string TransactionId { get; set; } = string.Empty;
        [Required]
        public string Custom { get; set; } = string.Empty; 
    }
}
