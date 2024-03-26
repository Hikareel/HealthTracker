using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HealthTracker.Server.Modules.Community.Controllers
{
    [Route("api")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepository _chatRepository;
        public ChatController(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        [HttpPost("users/messages")]
        public async Task<ActionResult> SendMessage([FromBody] SendMessageDTO sendMessageDTO)
        {
            try
            {
                await _chatRepository.SendMessage(sendMessageDTO);
                return Created();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
            
        }

        [HttpGet("users/messages/{userFrom}/{userTo}")]
        public async Task<ActionResult<ChatMessagesDTO>> GetMessages(int userFrom, int userTo, [FromQuery] int pageNumber, [FromQuery] int pageSize = 10)
        {

            try
            {
                var messagestDto = await _chatRepository.GetMessages(userFrom, userTo, pageNumber, pageSize);
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
