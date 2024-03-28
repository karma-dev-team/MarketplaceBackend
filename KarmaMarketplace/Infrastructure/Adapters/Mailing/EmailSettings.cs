namespace KarmaMarketplace.Infrastructure.Adapters.Mailing
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; } = null!;
        public int SmtpPort { get; set; }   
        public string FromEmail { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!; 
    }
}
