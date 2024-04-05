namespace KarmaMarketplace.Infrastructure.Adapters.Payment
{
    public class PaymentResult
    {
        public bool Success { get; set; } 
        public string Message { get; set; } = string.Empty; // Сообщение об ошибке или описании успешного выполнения
        public string PaymentId { get; set; } = string.Empty; // Уникальный идентификатор транзакции
                                              // Другие свойства, специфичные для результата платежа
        public string LinkUrl { get; set; } = string.Empty;
        public string QrLinkUrl { get; set; } = string.Empty;
    }

    public class PaymentPayload
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty; 
        public string OrderId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Custom { get; set; } = string.Empty; 
        public Dictionary<string, string> AdditionalInfo { get; set; } = new(); 
    }

    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded,
        // Другие статусы в зависимости от платежной системы
    }
}
