using HealthTracker.Server.Core.Models;
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
        public async Task<ActionResult> SendMessage([FromBody] CreateMessageDTO sendMessageDTO)
        {
            try
            {
                var result = await _chatRepository.CreateMessage(sendMessageDTO);
                if (result != null)
                {
                    return CreatedAtAction(nameof(GetMessage), new { messageId = result.Id }, result);
                }
                else
                {
                    return NotFound("Post couldn't be created.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("users/messages/{messageId}")]
        public async Task<ActionResult<MessageDTO>> GetMessage(int messageId)
        {
            try
            {
                var result = await _chatRepository.GetMessage(messageId);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("Message not found.");
                }
                
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("users/messages/{userFrom}/{userTo}")]
        public async Task<ActionResult<List<MessageDTO>>> GetMessages(int userFrom, int userTo, [FromQuery] int pageNumber, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _chatRepository.GetMessages(userFrom, userTo, pageNumber, pageSize);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
                
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
