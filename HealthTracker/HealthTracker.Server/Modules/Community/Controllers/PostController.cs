using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Repositories;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult> CreatePost([FromBody] PostDTO postDTO)
        {
            var result = await _postRepository.CreatePost(postDTO);
            if (result != null)
            {
                return Ok(); //Zamienić później na CreatedAtAction()
            }
            else
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("users/{userId}/posts")]
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

        [HttpPost("users/posts/comment/{parentCommentId?}")]
        public async Task<ActionResult> CreateComment(int? parentCommentId, [FromBody] CommentDTO commentDTO)
        {
            try
            {
                await _postRepository.CreateComment(parentCommentId, commentDTO);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("users/posts/comment/{commentId}")]
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

        [HttpGet("users/posts/comment/{postId}")]
        public async Task<ActionResult<List<CommentDTO>>> GetCommentByPostId(int postId)
        {
            try
            {
                var result = await _postRepository.GetCommentByPostId(postId);
                if(result != null)
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



    }
}
