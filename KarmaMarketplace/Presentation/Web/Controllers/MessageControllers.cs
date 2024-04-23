using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Common.Models;
using KarmaMarketplace.Application.Messaging.Dto;
using KarmaMarketplace.Application.Messaging.Interfaces;
using KarmaMarketplace.Domain.Messging.Entities;
using KarmaMarketplace.Infrastructure.Caching;
using KarmaMarketplace.Presentation.Web.Schemas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace KarmaMarketplace.Presentation.Web.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagingService _messagingService;
        private readonly IUser _user; 
        private readonly IMessagesCache _messagesCache;
        private readonly ILogger _logger;  

        public MessagesController(IMessagingService messagingService, IUser user, IMessagesCache messagesCache, ILogger<MessagesController> logger) { 
            _messagingService = messagingService;
            _user = user;
            _messagesCache = messagesCache;
            _logger = logger;
        }

        [HttpGet("chat/{chatId}/messages")]
        public async Task<ActionResult<ICollection<MessageEntity>>> GetChatMessages(Guid chatId, [FromQuery] InputPagination pagination)
        {
            return Ok(await _messagingService
                .GetChatMessages()
                .Execute(new GetChatMessagesDto { ChatId = chatId, Start = pagination.Start, Ends = pagination.Ends }));
        }

        [HttpPost("chat/{chatId}/send")]
        public async Task<ActionResult<MessageEntity>> SendMessage([FromBody] SendMessageScheme model, Guid chatId)
        {
            return Ok(await _messagingService
                .SendMessage()
                .Execute (new SendMessageDto()
                {
                    ChatId = model.ChatId, 
                    //PurchaseId = model.PurchaseId, 
                    Image = model.Image, 
                    Text = model.Text, 
                    FromUserId = _user.Id, 
                })); 
        }


        [HttpGet("subscribe/sse")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task GetMessagesBySee()
        {
            if (_user.Id == null)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return; 
            } 
            Response.Headers.TryAdd("Content-Type", "text/event-stream");
            Response.Headers.TryAdd("Cache-Control", "no-cache");
            Response.Headers.TryAdd("Connection", "keep-alive");

            while (!HttpContext.RequestAborted.IsCancellationRequested)
            {
                var newMessages = await _messagesCache.GetNewMessages((Guid)_user.Id);

                foreach (var message in newMessages)
                {
                    var eventData = $"{message}\n\n";
                    await Response.Body.WriteAsync(Encoding.UTF8.GetBytes(eventData));
                    await Response.Body.FlushAsync();
                }

                await Task.Delay(1000); 
            }
        }
    }
}
