using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Server.Modules.PhysicalActivity.Models
{
    public class GoalType
    {
        public int Id { get; set; }
        [MaxLength(255, ErrorMessage = "Must be 255 characters or less!")]
        public string Name { get; set; }

        public virtual ICollection<Goal> Goals { get; } = new List<Goal>();
    }
}
