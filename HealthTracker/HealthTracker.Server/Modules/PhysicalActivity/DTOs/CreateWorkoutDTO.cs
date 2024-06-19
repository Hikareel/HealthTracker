using HealthTracker.Server.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Server.Modules.PhysicalActivity.Models
{
    public class CreateWorkoutDTO
    {
        public int UserId { get; set; }
        [MaxLength(255, ErrorMessage = "Must be 255 characters or less!")]
        public string WorkoutType { get; set; }
        public int? Duration { get; set; }
        public bool Done { get; set; }
        public DateTime? Date { get; set; }

    }
}
