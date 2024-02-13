using HealthTracker.Server.Modules.User.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Server.Modules.PhysicalActivity.Models
{
    public class Workouts //Imo do zmiany
    {
        public int WorkoutId { get; set; }
        public int UserId { get; set; }
        public string WorkoutType { get; set; }
        public int Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public bool IsDone { get; set; }
        public DateTime Date {  get; set; }

        [ForeignKey("UserId")]
        public virtual Users User { get; set; }
    }
}
