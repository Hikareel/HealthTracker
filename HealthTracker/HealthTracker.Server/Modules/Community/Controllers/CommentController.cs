using HealthTracker.Server.Core.Exceptions;
using HealthTracker.Server.Core.Exceptions.Community;
using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthTracker.Server.Modules.Community.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly ILogger<CommentController> _logger;
        public CommentController(IPostRepository postRepository, ILogger<CommentController> logger)
        {
            _postRepository = postRepository;
            _logger = logger;
        }

        [HttpPost("users/posts/comments/{parentCommentId}")]
        [HttpPost("users/posts/comments")]
        public async Task<ActionResult> CreateComment(int? parentCommentId, [FromBody] CreateCommentDTO commentDTO)
        {
            try
            {
                var result = await _postRepository.CreateComment(parentCommentId, commentDTO);
                return CreatedAtAction(nameof(GetComment), new { commentId = result.Id }, result);

            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error occurred during the create comment process for {DTO}.", commentDTO);
                return BadRequest(ex.InnerException?.Message ?? "Database error.");
            }
            catch (Exception ex) when (ex is CommentNotFoundException || ex is UserNotFoundException || ex is PostNotFoundException)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the create comment process for {DTO}.", commentDTO);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("users/posts/comments/{commentId}")]
        public async Task<ActionResult<CommentDTO>> GetComment(int commentId)
        {
            try
            {
                var result = await _postRepository.GetComment(commentId);
                return Ok(result);
            }
            catch (CommentNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the get comment process for {CommentId}.", commentId);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("users/posts/{postId}/comments")]
        public async Task<ActionResult<CommentFromPostDTO>> GetCommentsByPostId(int postId, [FromQuery] int pageNumber, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _postRepository.GetCommentsByPostId(postId, pageNumber, pageSize);
                return Ok(result);
            }
            catch (NullPageException ex)
            {
                return Ok();
            }
            catch (PostNotFoundException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the get comments for post process for {PostId}.", postId);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("users/posts/{postId}/comments/{parentCommentId}")]
        public async Task<ActionResult<List<CommentDTO>>> GetCommentsByParentCommentId(int postId, int parentCommentId)
        {
            try
            {
                var result = await _postRepository.GetCommentsByParentCommentId(postId, parentCommentId);
                return Ok(result);
            }
            catch (Exception ex) when (ex is CommentNotFoundException || ex is PostNotFoundException)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("users/posts/comments/{commentId}")]
        public async Task<ActionResult> DeleteComment(int commentId)
        {
            try
            {
                await _postRepository.DeleteComment(commentId);
                return Ok();
            }
            catch (CommentNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the delete comment process for {CommentId}.", commentId);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("users/{usersId}/posts/comments")]
        public async Task<ActionResult> DeleteUsersComment(int userId)
        {
            try
            {
                await _postRepository.DeleteUserComments(userId);
                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the delete users comments process for {UserId}.", userId);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("users/posts/{postId}/comments")]
        public async Task<ActionResult> DeleteCommentFromPost(int postId)
        {
            try
            {
                await _postRepository.DeleteCommentsFromPost(postId);
                return Ok();
            }
            catch (PostNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the delete comments under post process for {UserId}.", postId);
                return StatusCode(500, "Internal server error.");
            }
        }

    }
}
