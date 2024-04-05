using KarmaMarketplace.Domain.Market.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Application.Payment.Dto
{
    public class BalanceOperationDto
    {
        [Required]
        public Guid WalletId {  get; set; }
        [Required]
        public Money Balance { get; set; } = new(0); 
    }

    public class GetWalletDto
    {
        public Guid WalletId { get; set; }
    }
}
