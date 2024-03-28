using Microsoft.Extensions.Options;

namespace KarmaMarketplace.Infrastructure.Adapters.Mailing
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMailing(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddSingleton<IEmailService, MailKitEmailService>(serviceProvider =>
            {
                var emailSettings = serviceProvider.GetRequiredService<IOptions<EmailSettings>>().Value;
                return new MailKitEmailService(emailSettings.SmtpServer, emailSettings.SmtpPort,
                                                emailSettings.FromEmail, emailSettings.Username, emailSettings.Password);
            });
            return services; 
        }
    }
}
