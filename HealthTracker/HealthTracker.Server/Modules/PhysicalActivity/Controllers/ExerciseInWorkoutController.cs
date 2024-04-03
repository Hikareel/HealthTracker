using HealthTracker.Server.Modules.PhysicalActivity.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HealthTracker.Server.Modules.PhysicalActivity.Controllers
{
    [Route("api")]
    [ApiController]
    public class ExerciseInWorkoutController : ControllerBase
    {
        private readonly IExerciseInWorkoutRepository _exerciseInWorkoutRepository;

        public ExerciseInWorkoutController(IExerciseInWorkoutRepository exerciseInWorkoutRepository)
        {
            _exerciseInWorkoutRepository = exerciseInWorkoutRepository;
        }
    }
}
