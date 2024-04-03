using HealthTracker.Server.Core.Exceptions.PhysicalActivity;
using HealthTracker.Server.Modules.PhysicalActivity.Models;
using HealthTracker.Server.Modules.PhysicalActivity.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthTracker.Server.Modules.PhysicalActivity.Controllers
{
    [Route("api")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutRepository _workoutRepository;
        public WorkoutController(IWorkoutRepository workoutRepository) 
        {
            _workoutRepository = workoutRepository;
        }

        [HttpPut("users/workouts")]
        public async Task<ActionResult> CreateExercise([FromBody] CreateWorkoutDTO createWorkoutDTO)
        {
            try
            {
                var result = await _workoutRepository.CreateWorkout(createWorkoutDTO);
                return CreatedAtAction(nameof(GetWorkout), new { id = result.Id }, result);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.InnerException?.Message ?? "Database error.");
            }
            catch (ExerciseTypeNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("users/workouts/{id}")]
        public async Task<ActionResult<WorkoutDTO>> GetWorkout(int id)
        {
            try
            {
                var result = await _workoutRepository.GetWorkout(id);
                return Ok(result);
            }
            catch (ExerciseNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        
    }
}
