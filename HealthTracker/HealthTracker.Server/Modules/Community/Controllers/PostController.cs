using HealthTracker.Server.Core.Exceptions;
using HealthTracker.Server.Core.Exceptions.Community;
using HealthTracker.Server.Core.Models;
using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Models;
using HealthTracker.Server.Modules.Community.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//Do zastanowienia czy oddzielić komentarze od postów (repozytorium i kontroler)

namespace HealthTracker.Server.Modules.Community.Controllers
{
    [Route("api")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly ILogger<PostController> _logger;
        public PostController(IPostRepository postRepository, ILogger<PostController> logger)
        {
            _postRepository = postRepository;
            _logger = logger;
        }


        [HttpPost("users/posts")]
        public async Task<ActionResult<PostDTO>> CreatePost([FromBody] CreatePostDTO postDTO)
        {
            try
            {
                var result = await _postRepository.CreatePost(postDTO);
                return CreatedAtAction(nameof(GetPost), new { postId = result.Id }, result);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error occurred during the create post process for {DTO}.", postDTO);
                return BadRequest(ex.InnerException?.Message ?? "Database error.");
            }
            catch (UserNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the create post process for {DTO}.", postDTO);
                return StatusCode(500, "Internal server error.");
            }

        }

        [HttpGet("users/posts/{postId}")]
        public async Task<ActionResult<PostDTO>> GetPost(int id)
        {
            try
            {
                var result = await _postRepository.GetPost(id);
                return Ok(result);
            }
            catch (PostNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the get post process for {PostId}.", id);
                return StatusCode(500, "Internal server error.");
            }

        }

        [HttpDelete("users/posts/{postId}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            try
            {
                await _postRepository.DeletePost(id);
                return Ok();
            }
            catch(PostNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the delete post process for {PostId}.", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("users/{userId}/wall/posts")]
        public async Task<ActionResult<PostListDTO>> GetPosts(int userId, [FromQuery] int pageNumber, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _postRepository.GetPosts(userId, pageSize, pageNumber);
                return Ok(result);

            }
            catch (UserNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the get posts process for user {UserId}.", userId);
                return StatusCode(500, "Internal server error.");
            }

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
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the create comment process for {DTO}.", commentDTO);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("users/posts/comments/{commentId}")]
        public async Task<ActionResult<CommentDTO>> GetComment(int id)
        {
            try
            {
                var result = await _postRepository.GetComment(id);
                return Ok(result);
            }
            catch (CommentNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the get comment process for {CommentId}.", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        //Tu może zmienić zwracanego jsona, aby zwracał zagnieżdżone komentarze dzieci
        [HttpGet("users/posts/{postId}/comments")]
        public async Task<ActionResult<List<CommentDTO>>> GetCommentsByPostId(int id)
        {
            try
            {
                var result = await _postRepository.GetCommentsByPostId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the get comments for post process for {PostId}.", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("users/posts/comments/{commentId}")]
        public async Task<ActionResult> DeleteComment(int id)
        {
            try
            {
                await _postRepository.DeleteComment(id);
                return Ok();
            }
            catch (CommentNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the delete comment process for {CommentId}.", id);
                return StatusCode(500, "Internal server error.");
            }
        }


    }
}
