using HealthTracker.Server.Modules.User.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Server.Modules.PhysicalActivity.Models
{
    public class Goals
    {
        public int GoalId {  get; set; }
        public int UserId { get; set; }
        public int GoalTypeId { get; set; }
        public string Content { get; set; }
        public DateTime DateOfCreation {  get; set; }
        public int Duration { get; set; }
        public bool IsDone { get; set; }

        [ForeignKey("UserId")]
        public virtual Users User { get; set; }

        [ForeignKey("GoalTypeId")]
        public virtual GoalTypes GoalType { get; set; }
    }
}
