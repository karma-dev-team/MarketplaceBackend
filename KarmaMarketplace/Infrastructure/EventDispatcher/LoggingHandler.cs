using Newtonsoft.Json;

namespace KarmaMarketplace.Infrastructure.EventDispatcher
{
    //public class LoggingHandler<Event> : IEventSubscriber<Event> where Event : BaseEvent
    //{
    //    private ILogger _logger;

    //    public LoggingHandler(ILogger<LoggingHandler<Event>> logger)
    //    {
    //        _logger = logger;
    //    }

    //    public Task HandleEvent(Event eventData)
    //    {
    //        var data = JsonConvert.SerializeObject(eventData);

    //        _logger.LogInformation($"Event: {eventData.GetType()} has happend, data: {data}");

    //        return Task.CompletedTask;
    //    }
    //}
}
