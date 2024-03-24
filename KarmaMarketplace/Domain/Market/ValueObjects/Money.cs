using KarmaMarketplace.Domain.Payment.Enums;

namespace KarmaMarketplace.Domain.Market.ValueObjects
{
    public class Money
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }

        public Money(decimal amount, CurrencyEnum currency = CurrencyEnum.RussianRuble)
        {
            Amount = amount;
            Currency = currency.ToString();
        }
    }
}
