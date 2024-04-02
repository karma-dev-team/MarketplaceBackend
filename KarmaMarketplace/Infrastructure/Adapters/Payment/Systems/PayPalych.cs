using Newtonsoft.Json.Linq;

namespace KarmaMarketplace.Infrastructure.Adapters.Payment.Systems
{
    public class PayPalychConfiguration
    {
        public string ShopId { get; set; } = null!;
        public string HostUrl { get; set; } = null!;
        public string SuccessUrl { get; set; } = null!; 
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

        public async Task<PaymentResult> InitPayment(decimal amount, string currency, string description, Dictionary<string, object> additionalParameters = null)
        {
            var data = new Dictionary<string, string>
            {
                {"amount", payload.Amount.ToString()},
                {"order_id", payload.OrderId},
                {"description", $"Оплата за {payload.Name}"},
                {"type", "normal"},
                {"shop_id", _shopId},
                {"custom", payload.Custom},
                {"name", payload.Name},
                {"success_url", _successUrl},
                {"fail_url", _failUrl}
            };

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);

            var response = await _httpClient.PostAsync(_hostUrl + _billUrl, new FormUrlEncodedContent(data));
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseData = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent);

            return new PaymentProcessingData
            {
                LinkUrl = responseData["link_page_url"],
                QrLinkUrl = responseData["link_url"],
                PaymentId = responseData["bill_id"],
                Success = responseData["success"] == "true",
                Status = "success"
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
