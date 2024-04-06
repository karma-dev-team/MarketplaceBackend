namespace KarmaMarketplace.Application.Payment.Exceptions
{
    public class TransactionInvalid : Exception
    {
        public string Custom { get; }
        public string CustomId { get; }

        public TransactionInvalid(string custom, string customId)
            : base("Transaction is invalid.")
        {
            Custom = custom;
            CustomId = customId;
        }

        public TransactionInvalid(string custom, string customId, string message)
            : base(message)
        {
            Custom = custom;
            CustomId = customId;
        }

        // Дополнительные конструкторы или свойства, если нужно
    }
}
