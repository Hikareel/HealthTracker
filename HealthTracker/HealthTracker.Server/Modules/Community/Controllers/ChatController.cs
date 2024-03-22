using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HealthTracker.Server.Modules.Community.Controllers
{
    [Route("api")]
    [ApiController]
    public class ChatController : Controller
    {
        private readonly IChatRepository _chatRepository;
        private const int pageSize = 10;
        public ChatController(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        [HttpPost("users/messages")]
        public async Task<ActionResult<SendMessageDTO>> SendMessage([FromBody] SendMessageDTO sendMessageDTO)
        {

            var result = await _chatRepository.SendMessage(sendMessageDTO);

            if (result)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet("users/{userId}/{friendId}/{pageNumber}")]
        public async Task<ActionResult<ChatMessagesDTO>> GetMessages(int userId, int friendId, int pageNumber)
        {

            try
            {
                var messagestDto = await _chatRepository.GetMessages(userId, friendId, pageNumber, pageSize);
                if (messagestDto == null)
                {
                    return NotFound();
                }
                return Ok(messagestDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
