using HealthTracker.Server.Core.Exceptions;
using HealthTracker.Server.Core.Exceptions.PhysicalActivity;
using HealthTracker.Server.Modules.PhysicalActivity.DTOs;
using HealthTracker.Server.Modules.PhysicalActivity.Models;
using HealthTracker.Server.Modules.PhysicalActivity.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthTracker.Server.Modules.PhysicalActivity.Controllers
{
    [Route("api")]
    [ApiController]
    public class GoalController : ControllerBase
    {
        private readonly IGoalRepository _goalRepository;
        public GoalController(IGoalRepository goalRepository)
        {
            _goalRepository = goalRepository;
        }
        
        [HttpPost("users/goals")]
        public async Task<ActionResult> CreateGoal([FromBody] CreateGoalDTO createGoalDTO)
        {
            try
            {
                var result = await _goalRepository.CreateGoal(createGoalDTO);
                return CreatedAtAction(nameof(GetGoal), new { id = result.Id }, result);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.InnerException?.Message ?? "Database error.");
            }
            catch (GoalTypeNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("users/goals/{id}")]
        public async Task<ActionResult<GoalDTO>> GetGoal(int id)
        {
            try
            {
                var result = await _goalRepository.GetGoal(id);
                return Ok(result);
            }
            catch (GoalNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }
        [HttpGet("users/{userId}/goals")]
        public async Task<ActionResult<List<GoalDTO>>> GetUsersGoals(int userId)
        {
            try
            {
                var result = await _goalRepository.GetUsersGoals(userId);
                return Ok(result);
            }
            catch(UserNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("users/goals")]
        public async Task<ActionResult<GoalDTO>> ChangeGoalStatus([FromBody] ChangeGoalDTO changeGoalDTO)
        {
            try
            {
                var result = await _goalRepository.ChangeGoalStatus(changeGoalDTO);
                return Ok(result);
            }
            catch(DbUpdateException ex)
            {
                return BadRequest(ex.InnerException?.Message ?? "Database error.");
            }
            catch (GoalNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }
        [HttpDelete("users/goals/{goalId}")]
        public async Task<ActionResult> DeleteGoal(int goalId)
        {
            try
            {
                await _goalRepository.DeleteGoal(goalId);
                return Ok();
            }
            catch (GoalNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost("users/goals/types")]
        public async Task<ActionResult> CreateGoalType([FromBody] CreateGoalTypeDTO createGoalTypeDTO)
        {
            try
            {
                var result = await _goalRepository.CreateGoalType(createGoalTypeDTO);
                return CreatedAtAction(nameof(GetGoalType), new { id = result.Id }, result);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.InnerException?.Message ?? "Database error.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("users/goals/types/{id}")]
        public async Task<ActionResult<GoalTypeDTO>> GetGoalType(int id)
        {
            try
            {
                var result = await _goalRepository.GetGoalType(id);
                return Ok(result);
            }
            catch (GoalTypeNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }

        }

        [HttpGet("users/goals/types")]
        public async Task<ActionResult<List<GoalTypeDTO>>> GetGoalTypes([FromQuery] int pageNumber, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _goalRepository.GetGoalTypes(pageNumber, pageSize);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }


    }
}
