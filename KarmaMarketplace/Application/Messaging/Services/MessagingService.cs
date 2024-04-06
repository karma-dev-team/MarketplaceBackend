using KarmaMarketplace.Application.Messaging.Interfaces;
using KarmaMarketplace.Application.Messaging.UseCases;

namespace KarmaMarketplace.Application.Messaging.Services
{
    public class MessagingService : IMessagingService
    {
        private IServiceProvider _serviceProvider; 

        public MessagingService(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider; // beeline!!! 
        }

        public SendMessage SendMessage()
        {
            return _serviceProvider.GetRequiredService<SendMessage>();  
        }
        public GetChatMessages GetChatMessages()
        {
            return _serviceProvider.GetRequiredService<GetChatMessages>();
        }
        public GetChat GetChat()
        {
            return _serviceProvider.GetRequiredService<GetChat>(); 
        }
        public GetChatsList GetChatsList()
        {
            return _serviceProvider.GetRequiredService<GetChatsList>();
        }

        public InitiateProductChat InitiateProductChat()
        {
            return _serviceProvider.GetRequiredService<InitiateProductChat>(); 
        }
    }
}
