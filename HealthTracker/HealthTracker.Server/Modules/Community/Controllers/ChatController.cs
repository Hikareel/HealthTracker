using HealthTracker.Server.Core.Controllers;
using HealthTracker.Server.Core.Exceptions;
using HealthTracker.Server.Core.Exceptions.Community;
using HealthTracker.Server.Core.Models;
using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthTracker.Server.Modules.Community.Controllers
{
    [Route("api")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepository _chatRepository;
        private readonly ILogger<ChatController> _logger;
        public ChatController(IChatRepository chatRepository, ILogger<ChatController> logger)
        {
            _chatRepository = chatRepository;
            _logger = logger;
        }

        [HttpPost("users/messages")]
        public async Task<ActionResult> CreateMessage([FromBody] CreateMessageDTO sendMessageDTO)
        {
            try
            {
                var result = await _chatRepository.CreateMessage(sendMessageDTO);
                return CreatedAtAction(nameof(GetMessage), new { messageId = result.Id }, result);

            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error occurred during the create message process for {SendMessageDTO}.", sendMessageDTO);
                return BadRequest(ex.InnerException?.Message ?? "Database error.");
            }
            catch (UserNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the create message process for {DTO}.", sendMessageDTO);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("users/messages/{messageId}")]
        public async Task<ActionResult<MessageDTO>> GetMessage(int messageId)
        {
            try
            {
                var result = await _chatRepository.GetMessage(messageId);
                return Ok(result);

            }
            catch(MessageNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the get message process for {MessageId}.", messageId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("users/messages/{userFrom}/{userTo}")]
        public async Task<ActionResult<List<MessageDTO>>> GetMessages(int userFrom, int userTo, [FromQuery] int pageNumber, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _chatRepository.GetMessages(userFrom, userTo, pageNumber, pageSize);
                return Ok(result);
            }
            catch (UserNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the get messages process for {UserFrom} to {UserTo}.", userFrom, userTo);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
