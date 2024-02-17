using HealthTracker.Server.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace HealthTracker.Server.Modules.Community.Models
{
    public class Status
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255, ErrorMessage = "Must be 255 characters or less!")]
        public string Name { get; set; }
        public virtual ICollection<Friendship> Friendships { get; } = new List<Friendship>();
        public virtual ICollection<Notification> Notifications { get; } = new List<Notification>();
    }
}
