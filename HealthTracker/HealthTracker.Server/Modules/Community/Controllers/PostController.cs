using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Models;
using HealthTracker.Server.Modules.Community.Repositories;
using Microsoft.AspNetCore.Mvc;

//Do zastanowienia czy oddzielić komentarze od postów (repozytorium i kontroler)

namespace HealthTracker.Server.Modules.Community.Controllers
{
    [Route("api")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }


        [HttpPost("users/posts")]
        public async Task<ActionResult<PostDTO>> CreatePost([FromBody] CreatePostDTO postDTO)
        {
            try
            {
                var result = await _postRepository.CreatePost(postDTO);
                if (result != null)
                {
                    return CreatedAtAction(nameof(GetPost), new { postId = result.Id }, result);
                }
                else
                {
                    return BadRequest("Post couldn't be created because the user does not exist.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
            
        }

        [HttpGet("users/posts/{postId}")]
        public async Task<ActionResult<PostDTO>> GetPost(int postId)
        {
            try
            {
                var result = await _postRepository.GetPost(postId);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("Post not found.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }

        }

        [HttpGet("users/{userId}/wall/posts")]
        public async Task<ActionResult<PostListDTO>> GetPosts(int userId,[FromQuery] int pageNumber, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _postRepository.GetPosts(userId, pageSize, pageNumber);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("Post not found.");
                }
            }
            catch (Exception) 
            {
                return StatusCode(500, "Internal server error.");
            }
            
        }

        [HttpPost("users/posts/comments/{parentCommentId?}")]
        [HttpPost("users/posts/comments")]
        public async Task<ActionResult> CreateComment(int? parentCommentId, [FromBody] CreateCommentDTO commentDTO)
        {
            try
            {
                var result = await _postRepository.CreateComment(parentCommentId, commentDTO);
                if (result != null)
                {
                    return CreatedAtAction(nameof(GetComment), new { commentId = result.Id }, result);
                }
                else
                {
                    return BadRequest("Comment couldn't be created because the user or post does not exist.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("users/posts/comments/{commentId}")]
        public async Task<ActionResult<CommentDTO>> GetComment(int commentId)
        {
            try
            {
                var result = await _postRepository.GetComment(commentId);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("Comment not found.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }
        //Tu może zmienić zwracanego jsona, aby zwracał zagnieżdżone komentarze dzieci
        [HttpGet("users/posts/{postId}/comments")]
        public async Task<ActionResult<List<CommentDTO>>> GetCommentsByPostId(int postId)
        {
            try
            {
                var result = await _postRepository.GetCommentsByPostId(postId);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("Comments not found.");
                }
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
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        



    }
}
