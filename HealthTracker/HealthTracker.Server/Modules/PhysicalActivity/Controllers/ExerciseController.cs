using HealthTracker.Server.Core.Exceptions.PhysicalActivity;
using HealthTracker.Server.Modules.PhysicalActivity.DTOs;
using HealthTracker.Server.Modules.PhysicalActivity.Models;
using HealthTracker.Server.Modules.PhysicalActivity.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthTracker.Server.Modules.PhysicalActivity.Controllers
{
    [Route("api")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseRepository _exerciseRepository;
        public ExerciseController(IExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        [HttpPost("exercises")]
        public async Task<ActionResult> CreateExercise([FromBody] CreateExerciseDTO createExerciseDTO)
        {
            try
            {
                var result = await _exerciseRepository.CreateExercise(createExerciseDTO);
                return CreatedAtAction(nameof(GetExercise), new { id = result.Id }, result);
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

        [HttpGet("exercises/{id}")]
        public async Task<ActionResult<ExerciseDTO>> GetExercise(int id)
        {
            try
            {
                var result = await _exerciseRepository.GetExercise(id);
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

        [HttpDelete("exercises/{id}")]
        public async Task<ActionResult> DeleteExercise(int id)
        {
            try
            {
                await _exerciseRepository.DeleteExercise(id);
                return Ok();
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


        [HttpPost("exercises/types")]
        public async Task<ActionResult> CreateExerciseType([FromBody] CreateExerciseTypeDTO createExerciseTypeDTO)
        {
            try
            {
                var result = await _exerciseRepository.CreateExerciseType(createExerciseTypeDTO);
                return CreatedAtAction(nameof(GetExerciseType), new { id = result.Id }, result);
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

        [HttpGet("exercises/types/{id}")]
        public async Task<ActionResult<ExerciseDTO>> GetExerciseType(int id)
        {
            try
            {
                var result = await _exerciseRepository.GetExerciseType(id);
                return Ok(result);
            }
            catch (ExerciseTypeNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("exercises/types/{id}")]
        public async Task<ActionResult> DeleteExerciseType(int id)
        {
            try
            {
                await _exerciseRepository.DeleteExerciseType(id);
                return Ok();
            }
            catch (ExerciseTypeNotFoundException ex)
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
