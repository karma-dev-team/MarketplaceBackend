using KarmaMarketplace.Application.Messaging.Dto;
using KarmaMarketplace.Application.Messaging.Interfaces;
using KarmaMarketplace.Domain.Messging.Entities;
using KarmaMarketplace.Presentation.Web.Schemas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarmaMarketplace.Presentation.Web.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private IMessagingService _messagingService;

        public MessagesController(IMessagingService messagingService) { 
            _messagingService = messagingService;
        }

        [HttpGet("chat/{chatId}/messages")]
        public async Task<ActionResult<ICollection<MessageEntity>>> GetChatMessages(Guid chatId)
        {
            return Ok(await _messagingService
                .GetChatMessages()
                .Execute(new GetChatMessagesDto { ChatId = chatId }));
        }

        [HttpPost("chat/{chatId}")]
        public async Task<ActionResult<MessageEntity>> SendMessage([FromBody] SendMessageScheme model, Guid chatId)
        {
            return Ok(await _messagingService
                .SendMessage()
                .Execute(new SendMessageDto()
                {
                    ChatId = model.ChatId, 
                    PurchaseId = model.PurchaseId, 
                    Image = model.Image, 
                    Text = model.Text
                })); 
        }

        [HttpPost("subscribe")]
        public async Task Subscribe()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }
    }
}
