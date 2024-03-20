namespace KarmaMarketplace.Domain.Common
{
    public class AccessDenied : Exception
    {
        public AccessDenied(string? info) 
            : base($"Access denied, not enough permissions, info: {info}") 
        {
        }
    }
}
