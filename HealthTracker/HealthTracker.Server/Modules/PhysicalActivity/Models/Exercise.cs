using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Server.Modules.PhysicalActivity.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        [ForeignKey("ExerciseTypeId")]
        public ExerciseType ExerciseType { get; set; }
        public int ExerciseTypeId { get; set; }
        public int? Series { get; set; }
        public int? Reps { get; set; }
        public int? Duration { get; set; }

        public virtual ICollection<Workout> Workouts { get; } = new List<Workout>();
    }
}
