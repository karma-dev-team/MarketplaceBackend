using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.User.UseCases;
using KarmaMarketplace.Domain.Market.Events;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Application.Market.EventHandlers
{
    public class ProductCreatedHandler : IEventSubscriber<ProductCreated>
    {
        private readonly ILogger _logger;
        // лучше отправить CreateNotification в NotificationService, и через него отсылать уведомления
        private readonly CreateNotification _createNotification; 

        public ProductCreatedHandler(ILogger<ProductCreatedHandler> logger, CreateNotification createNotification)
        {
            _logger = logger;
            _createNotification = createNotification;
        }

        public async Task HandleEvent(ProductCreated eventValue, IApplicationDbContext _context)
        {
            string message = $"User: {eventValue.Product.CreatedBy.Id}, created product: {eventValue.Product.Id}";
            _logger.LogInformation(message);
            await _createNotification.Execute(
                new User.Dto.CreateNotificationDto() { 
                    UserId = eventValue.Product.CreatedBy.Id,
                    Text = "Вы создали продукт", 
                    Data = { { "Имя продукта", eventValue.Product.Id.ToString() } }
                }
            ); 
        }
    }
}
