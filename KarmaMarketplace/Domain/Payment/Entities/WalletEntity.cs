using KarmaMarketplace.Domain.Payment.Enums;
using KarmaMarketplace.Domain.Payment.Events;
using KarmaMarketplace.Domain.Payment.Exceptions;
using KarmaMarketplace.Domain.Payment.ValueObjects;
using KarmaMarketplace.Domain.User.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmaMarketplace.Domain.Payment.Entities
{
    public class WalletEntity : BaseAuditableEntity
    {
        [ForeignKey("User")]
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public virtual UserEntity User { get; set; } = null!;
        [Required]
        public CurrencyEnum Currency { get; set; } = Enums.CurrencyEnum.RussianRuble; 
        [Required]
        public Money Frozen { get; set; } = new(0, Enums.CurrencyEnum.RussianRuble);
        [Required]
        public Money AvailableBalance { get; set; } = new(0, Enums.CurrencyEnum.RussianRuble); 
        [Required]
        public Money PendingIncome { get; set; } = new(0, Enums.CurrencyEnum.RussianRuble);
        [Required]
        public bool Blocked { get; set; } = false;

        public static WalletEntity Create(UserEntity user)
        {
            var wallet = new WalletEntity();

            wallet.User = user;

            wallet.AddDomainEvent(new WalletCreated(wallet)); 

            return wallet; 
        }

        public void AddBalance(Money amount)
        {
            AvailableBalance.Amount += amount.Amount;

            AddDomainEvent(new BalanceChanged(this, amount)); 
        }

        public void DecreaseBalance(Money money)
        {
            if (Blocked)
                throw new WalletIsBlocked(Id);

            if (AvailableBalance < money)
                throw new NotEnoughMoneyException(Id);

            AvailableBalance -= money;
            AddDomainEvent(new WalletBalanceDecreased(this, money));
        }

        public void IncreaseBalance(Money money)
        {
            if (Blocked)
                throw new WalletIsBlocked(Id);

            AvailableBalance += money;
            AddDomainEvent(new WalletBalanceIncreased(this, money));
        }

        public void RollbackTransaction(TransactionEntity transaction)
        {
            VerifyTransaction(transaction, raiseErr: true);

            if (transaction.Direction == TransactionDirection.In)
                AvailableBalance += transaction.Amount;
        }

        public void ConfirmTransaction(TransactionEntity transaction, WalletEntity? fromWallet = null)
        {
            VerifyTransaction(transaction, raiseErr: true);

            if (transaction.Direction == TransactionDirection.Out)
            {
                if (fromWallet != null)
                    fromWallet.AvailableBalance += transaction.Amount;
                DecreaseBalance(transaction.Amount);
            }
            else if (transaction.Direction == TransactionDirection.In)
            {
                if (fromWallet != null)
                    fromWallet.AvailableBalance -= transaction.Amount;
                IncreaseBalance(transaction.Amount);
            }

            AddDomainEvent(new ConfirmedTransaction(
                transaction: transaction, wallet: this, fromWallet: fromWallet));

            transaction.Status = TransactionStatusEnum.Confirmed;
        }

        public bool VerifyTransaction(TransactionEntity transaction, bool raiseErr = false)
        {
            bool fromUserWallet = transaction.CreatedByUser.Id == UserId;
            bool createdByUserWallet = transaction.CreatedById == UserId;
            bool result = fromUserWallet || createdByUserWallet;

            if (!result && raiseErr)
                throw new TransactionVerificationFailed(Id, transaction.Id);

            return result;
        }

        public void FreezeAmount(Money money)
        {
            if (Blocked)
                throw new WalletIsBlocked(Id);

            if (Currency != money.Currency)
                throw new ArgumentException("Currency is not the same");

            Frozen += money;
            AddDomainEvent(new WalletAmountFrozen(this, money));
        }

        public void UnfreezeAmount(Money money)
        {
            if (Blocked)
                throw new WalletIsBlocked(Id);

            if (Currency != money.Currency)
                throw new ArgumentException("Currency is not the same");

            if (Frozen - money < new Money(0, currency: money.Currency))
                throw new ArgumentException("Negative value amount");

            Frozen -= money;
            AddDomainEvent(new WalletUnfrozenAmount(this, money));
        }

        public void Block(string reason)
        {
            if (Blocked)
                throw new WalletIsBlocked(Id);

            Blocked = true;
            AddDomainEvent(new WalletBlocked(this, reason));
        }

        public void RecordAmount()
        {
            AddDomainEvent(new RecordWallet(this));
        }
    }
}
