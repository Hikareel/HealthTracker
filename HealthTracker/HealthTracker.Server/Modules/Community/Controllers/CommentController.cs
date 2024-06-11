﻿using HealthTracker.Server.Core.Exceptions;
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
                return BadRequest(ex.Message);
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
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the get comment process for {CommentId}.", commentId);
                return StatusCode(500, "Internal server error.");
            }
        }

        //Tu może zmienić zwracanego jsona, aby zwracał zagnieżdżone komentarze dzieci
        [HttpGet("users/posts/{postId}/comments")]
        public async Task<ActionResult<List<CommentDTO>>> GetCommentsByPostId(int commentId)
        {
            try
            {
                var result = await _postRepository.GetCommentsByPostId(commentId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the get comments for post process for {PostId}.", commentId);
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
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the delete comment process for {CommentId}.", commentId);
                return StatusCode(500, "Internal server error.");
            }
        }


    }
}
