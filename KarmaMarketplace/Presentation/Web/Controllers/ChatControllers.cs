using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Messaging.Dto;
using KarmaMarketplace.Application.Messaging.Interfaces;
using KarmaMarketplace.Domain.Messging.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarmaMarketplace.Presentation.Web.Controllers
{
    [Route("api/chat/")]
    [ApiController]
    public class ChatControllers : ControllerBase
    {
        private IMessagingService _service; 

        public ChatControllers(IMessagingService service)
        {
            _service = service; 
        }

        [HttpGet("{chatId}")]
        public async Task<ActionResult<ChatEntity>> GetChatById(Guid chatId)
        {
            return Ok(await _service
                .GetChat()
                .Execute(new GetChatDto() { ChatId = chatId })); 
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ICollection<ChatEntity>>> GetChatsByUser(Guid userId)
        {
            return Ok(await _service
                .GetChatsList()
                .Execute(new GetChatsListDto() { UserId = userId })); 
        }
    }
}
