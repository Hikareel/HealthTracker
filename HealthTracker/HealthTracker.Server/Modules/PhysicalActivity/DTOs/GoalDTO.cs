using HealthTracker.Server.Core.Models;
using HealthTracker.Server.Modules.PhysicalActivity.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthTracker.Server.Modules.PhysicalActivity.DTOs
{
    public class GoalDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GoalTypeId { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "Must be 1000 characters or less!")]
        public string Content { get; set; }
        public DateTime? DateOfEnd { get; set; }
        public bool IsDone { get; set; }
    }
}
