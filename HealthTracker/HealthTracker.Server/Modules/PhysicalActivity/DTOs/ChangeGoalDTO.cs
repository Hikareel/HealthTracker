using HealthTracker.Server.Core.Models;
using HealthTracker.Server.Modules.PhysicalActivity.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthTracker.Server.Modules.PhysicalActivity.DTOs
{
    public class ChangeGoalDTO
    {
        public int Id { get; set; }
        public int? GoalTypeId { get; set; }
        public string? Content { get; set; }
        public DateTime? DateOfEnd { get; set; }
        public bool? IsDone { get; set; }
    }
}
