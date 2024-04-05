namespace KarmaMarketplace.Domain.Payment.Exceptions
{
    public class PurchaseIsAlreadyCompleted : Exception
    {
        public Guid PurchaseId { get; set; }

        public PurchaseIsAlreadyCompleted(Guid purchaseId) 
            : base($"Purchase is already completed, purchaseid: {purchaseId}")
        {
            PurchaseId = purchaseId;
        }
    }
}
