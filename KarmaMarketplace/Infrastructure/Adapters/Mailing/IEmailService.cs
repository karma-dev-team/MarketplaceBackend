namespace KarmaMarketplace.Infrastructure.Adapters.Mailing
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
