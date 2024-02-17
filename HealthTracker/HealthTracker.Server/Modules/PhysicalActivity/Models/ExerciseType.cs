using System.ComponentModel.DataAnnotations;

namespace HealthTracker.Server.Modules.PhysicalActivity.Models
{
    public class ExerciseType
    {
        public int Id { get; set; }
        public int? CaloriesBurned { get; set; }
        public bool IsTimeBased { get; set; }
        [Required]
        [MaxLength(255, ErrorMessage = "Must be 255 characters or less!")]
        public string Name { get; set; }

        public virtual ICollection<Exercise> Exercises { get; } = new List<Exercise>();
    }
}
