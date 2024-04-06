using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Application.Payment.Dto
{
    public class ConfirmPurchaseDto
    {
        [Required]
        public Guid PurchaseId { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        [Range(0, 5, ErrorMessage = "Rate had to be 1-5")]
        public int Rate { get; set; }
        [Required]
        public string RateText { get; set; } = string.Empty;
    }

    public class CreatePurchaseDto
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public string ProviderName { get; set; } = string.Empty;
    }

    public class UpdatePurchaseDto
    {
        
    }

    public class GetPurchasesListDto
    {

    }


}
