using KarmaMarketplace.Domain.Payment.Enums;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Domain.Market.ValueObjects
{
    [Owned]
    public class Money
    {
        public decimal Amount { get; set; }
        public CurrencyEnum Currency { get; set; }

        public Money(decimal amount, CurrencyEnum currency = CurrencyEnum.RussianRuble)
        {
            Amount = amount;
            Currency = currency; 
        }
    }
}
