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

        public static void VerifyCurrency(Money lhs, Money rhs)
        {
            if (lhs.Currency != rhs.Currency)
            {
                throw new ArgumentException("Currencies is not equal"); 
            }
        }

        public static bool operator >(Money lhs, Money rhs)
        {
            VerifyCurrency(lhs, rhs);
            return lhs.Amount > rhs.Amount;
        }

        public static bool operator <(Money lhs, Money rhs)
        {
            VerifyCurrency(lhs, rhs);
            return lhs.Amount < rhs.Amount;
        }

        public static bool operator ==(Money lhs, Money rhs)
        {
            VerifyCurrency(lhs, rhs);
            return lhs.Amount == rhs.Amount;
        }

        public static bool operator !=(Money lhs, Money rhs)
        {
            VerifyCurrency(lhs, rhs);
            return lhs.Amount != rhs.Amount;
        }

        public static Money operator -(Money lhs, Money rhs)
        {
            VerifyCurrency(lhs, rhs);
            return new Money(lhs.Amount - rhs.Amount, lhs.Currency);
        }

        public static Money operator +(Money lhs, Money rhs)
        {
            VerifyCurrency(lhs, rhs);
            return new Money(lhs.Amount + rhs.Amount, lhs.Currency);
        }
    }
}
