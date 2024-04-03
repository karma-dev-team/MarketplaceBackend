namespace KarmaMarketplace.Infrastructure.Adapters.Payment
{
    public interface IPaymentAdapter
    {
        // Инициирует платеж и возвращает результат операции.
        // В параметры можно включить сумму, валюту, описание платежа и дополнительные параметры, 
        // специфичные для каждой платежной системы.
        Task<PaymentResult> InitPayment(PaymentPayload payload);

        // Метод для проверки статуса платежа, если это требуется по логике платежной системы.
        // Может возвращать статус платежа в удобочитаемом формате.
        Task<PaymentStatus> CheckPaymentStatus(string paymentId);

        // Опциональный метод для возврата средств, если такая возможность поддерживается платежной системой.
        // Возвращает результат операции возврата.
        Task<PaymentResult> RefundPayment(string paymentId, decimal amount);
    }
}
