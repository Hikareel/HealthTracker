using HealthTracker.Server.Modules.Community.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Server.Core.Models
{
    public class Notification
    {
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }
        [MaxLength(1000, ErrorMessage = "Must be 1000 characters or less!")]
        public string Content { get; set; }
        [ForeignKey("StatusId")]
        public Status Status { get; set; }
        public int StatusId { get; set; }
        public DateTime? DateOfCreate { get; set; }

    }
}
