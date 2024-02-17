using HealthTracker.Server.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Server.Modules.PhysicalActivity.Models
{
    public class Goal
    {
        public int Id { get; set; }
        [ForeignKey("UserId")]
        [Column(Order = 1)]
        public User User { get; set; }
        public int UserId { get; set; }
        [ForeignKey("GoalTypeId")]
        [Column(Order = 2)]
        public GoalType GoalType { get; set; }
        public int GoalTypeId { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "Must be 1000 characters or less!")]
        public string Content { get; set; }
        public DateTime? DateOfEnd { get; set; }
        public bool IsDone { get; set; }
    }
}
