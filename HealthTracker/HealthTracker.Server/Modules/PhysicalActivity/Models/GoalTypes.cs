using HealthTracker.Server.Modules.User.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Server.Modules.PhysicalActivity.Models
{
    public class GoalTypes
    {
        public int GoalTypeId {  get; set; }
        public string Name { get; set; }
    }
}
