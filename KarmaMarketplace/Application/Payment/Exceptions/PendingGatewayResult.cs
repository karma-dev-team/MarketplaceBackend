namespace KarmaMarketplace.Application.Payment.Exceptions
{
    public class PendingGatewayResult : Exception
    {
        public PendingGatewayResult(Guid transactionId) 
            : base($"Transaction: {transactionId} was not confirmed by payment system") { }
    }
}
