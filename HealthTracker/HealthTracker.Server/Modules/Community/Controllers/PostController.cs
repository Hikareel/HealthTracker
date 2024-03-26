using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HealthTracker.Server.Modules.Community.Controllers
{
    [Route("api")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private const int pageSize = 10;
        public PostController(IPostRepository postRepository) 
        {
            _postRepository = postRepository;
        }


        [HttpPost("users/posts")]
        public async Task<ActionResult<PostDTO>> CreatePost([FromBody] PostDTO postDTO)
        {
            var result = await _postRepository.CreatePost(postDTO);
            if (result != null) 
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("users/{userId}/posts/{pageNumber}")]
        public async Task<ActionResult<PostListDTO>> GetPosts(int userId, int pageNumber)
        {
            var result = await _postRepository.GetPosts(userId, pageSize, pageNumber);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
