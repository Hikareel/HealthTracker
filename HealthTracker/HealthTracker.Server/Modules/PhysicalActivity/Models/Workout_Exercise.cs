using HealthTracker.Server.Modules.User.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Server.Modules.PhysicalActivity.Models
{
    public class Workout_Exercise
    {
        public int WorkoutId { get; set; }
        public int ExerciseId { get; set; }

        [ForeignKey("UserId")]
        public virtual Users User { get; set; }
        [ForeignKey("ExerciseId")]
        public virtual Exercises Exercise { get; set; }
    }
}
