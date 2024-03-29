using KarmaMarketplace.Application.Messaging.UseCases;

namespace KarmaMarketplace.Application.Messaging.Interfaces
{
    public interface IMessagingService
    {
        SendMessage SendMessage(); 
        GetChatMessages GetChatMessages();
        GetChat GetChat();
        GetChatsList GetChatsList();
    }
}
