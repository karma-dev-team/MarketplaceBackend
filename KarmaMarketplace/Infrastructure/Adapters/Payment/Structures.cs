namespace KarmaMarketplace.Infrastructure.Adapters.Payment
{
    public class PaymentResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } // Сообщение об ошибке или описании успешного выполнения
        public string PaymentId { get; set; } // Уникальный идентификатор транзакции
                                              // Другие свойства, специфичные для результата платежа
        public string PaymentUrl { get; set; }
        public string QuLinkUrl { get; set; }
    }

    public class PaymentPayload
    {
        public decimal Amount { get; set; } 
        public string OrderId { get; set; } = string.Empty;
        public string Name { get; set; }
        public string Custom { get; set; }
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
