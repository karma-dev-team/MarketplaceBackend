namespace KarmaMarketplace.Domain.Payment.Exceptions
{
    public class TransactionVerificationFailed : Exception
    {
        public Guid UserId { get; set; }
        public Guid TransactionId { get; set; }

        public TransactionVerificationFailed(Guid userId, Guid transactionId) : base("Transaction verification failed")
        {
            UserId = userId;
            TransactionId = transactionId;
        }
    }
}
