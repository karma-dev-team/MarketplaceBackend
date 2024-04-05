using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Application.Payment.Dto
{
    public class ConfirmPurchaseDto
    {
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
