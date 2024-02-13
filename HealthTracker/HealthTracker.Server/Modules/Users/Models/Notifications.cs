using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Server.Modules.User.Models
{
    public class Notifications
    {
        [Required]
        public int NotificationsId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users User { get; set; }

        public string Text { get; set; }
        public string Status { get; set; }
        public DateTime DateOfCreation { get; set; }

    }
}
