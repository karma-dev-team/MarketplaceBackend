using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Messaging.Dto;
using KarmaMarketplace.Application.Messaging.Interfaces;
using KarmaMarketplace.Domain.Messging.Entities;
using KarmaMarketplace.Presentation.Web.Schemas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarmaMarketplace.Presentation.Web.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private IMessagingService _messagingService;
        private IUser _user; 

        public MessagesController(IMessagingService messagingService, IUser user) { 
            _messagingService = messagingService;
            _user = user; 
        }

        [HttpGet("chat/{chatId}/messages")]
        public async Task<ActionResult<ICollection<MessageEntity>>> GetChatMessages(Guid chatId)
        {
            return Ok(await _messagingService
                .GetChatMessages()
                .Execute(new GetChatMessagesDto { ChatId = chatId }));
        }

        [HttpPost("chat/{chatId}/send")]
        public async Task<ActionResult<MessageEntity>> SendMessage([FromBody] SendMessageScheme model, Guid chatId)
        {
            return Ok(await _messagingService
                .SendMessage()
                .Execute(new SendMessageDto()
                {
                    ChatId = model.ChatId, 
                    //PurchaseId = model.PurchaseId, 
                    Image = model.Image, 
                    Text = model.Text, 
                    FromUserId = _user.Id, 
                })); 
        }

        [HttpPost("subscribe")]
        [Authorize(AuthenticationSchemes = "Bearer")]
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
