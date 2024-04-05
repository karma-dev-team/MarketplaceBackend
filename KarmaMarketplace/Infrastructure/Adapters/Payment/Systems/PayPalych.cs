using Newtonsoft.Json.Linq;

namespace KarmaMarketplace.Infrastructure.Adapters.Payment.Systems
{
    public class PayPalychConfiguration
    {
        public string ShopId { get; set; } = null!;
        public string HostUrl { get; set; } = null!;
        public string SuccessUrl { get; set; } = null!; 
        public string AccessToken {  get; set; } = null!;
    } 

    public class PayPalychPaymentAdapter : IPaymentAdapter
    {
        private HttpClient _httpClient;
        private PayPalychConfiguration _config;

        public PayPalychPaymentAdapter(HttpClient httpClient, PayPalychConfiguration config) 
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<PaymentResult> InitPayment(PaymentPayload payload)
        {
            var data = new Dictionary<string, string>
            {
                {"amount", payload.Amount.ToString()},
                {"order_id", payload.OrderId},
                {"description", $"Оплата за {payload.Name}"},
                {"type", "normal"},
                {"shop_id", _config.ShopId},
                {"custom", payload.Custom},
                {"name", payload.Name},
                {"success_url", _config.SuccessUrl},
                {"fail_url", _config.SuccessUrl}
            };

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer", _config.AccessToken);

            var response = await _httpClient.PostAsync(
                _config.HostUrl + _config.SuccessUrl, new FormUrlEncodedContent(data));
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseData = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent);

            Guard.Against.Null(responseData, message: "Paypalych no answer "); 

            return new PaymentResult
            {
                LinkUrl = responseData["link_page_url"],
                QrLinkUrl= responseData["link_url"],
                PaymentId = responseData["bill_id"],
                Success = responseData["success"] == "true",
            };
        }

        public async Task<PaymentStatus> CheckPaymentStatus(string paymentId)
        {
            return PaymentStatus.Pending;
        }

        public Task<PaymentResult> RefundPayment(string paymentId, decimal amount)
        {
            throw new NotImplementedException();  
        }
    }
}
