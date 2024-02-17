using HealthTracker.Server.Modules.Community.Models;
using HealthTracker.Server.Modules.Health.Models;
using HealthTracker.Server.Modules.PhysicalActivity.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HealthTracker.Server.Core.Models
{
    public class User : IdentityUser<int>
    {
        
        [MaxLength(100, ErrorMessage = "Must be 100 characters or less!")]
        public string FirstName { get; set; }
        [MaxLength(100, ErrorMessage = "Must be 100 characters or less!")]
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfCreate { get; set; }

        public virtual ICollection<Goal> Goals { get; } = new List<Goal>();
        public virtual ICollection<Workout> Workouts { get; } = new List<Workout>();
        public virtual ICollection<Post> Posts { get; } = new List<Post>();
        public virtual ICollection<Comment> Comments { get; } = new List<Comment>();
        public virtual ICollection<Notification> Notifications { get; } = new List<Notification>();
        public virtual ICollection<Friendship> Friendships { get; } = new List<Friendship>();
        public virtual ICollection<HealthMeasurement> HealthMeasurements { get; } = new List<HealthMeasurement>();
    }

}
