namespace KarmaMarketplace.Domain.Market.Exceptions
{
    public class IncorrectAttributes : Exception
    {
        public IncorrectAttributes(string field, string? value) 
            : base($"Incorrect attributes, field: {field}, value: {value}") 
        {
        }
    }
}
