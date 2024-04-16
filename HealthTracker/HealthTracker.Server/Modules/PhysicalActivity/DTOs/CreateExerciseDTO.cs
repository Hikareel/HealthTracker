using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Server.Modules.PhysicalActivity.Models
{
    public class CreateExerciseDTO
    {
        public int ExerciseTypeId { get; set; }
        public int? Series { get; set; }
        public int? Reps { get; set; }
        public int? Duration { get; set; }
    }
}
