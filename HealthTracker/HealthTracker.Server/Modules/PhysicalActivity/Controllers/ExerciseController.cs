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

        [HttpPut("exercise")]
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

        [HttpGet("exercise/{id}")]
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

        [HttpDelete("exercise/{id}")]
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


        [HttpPut("exercise/type")]
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

        [HttpGet("exercise/type/{id}")]
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

        [HttpDelete("exercise/type/{id}")]
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
