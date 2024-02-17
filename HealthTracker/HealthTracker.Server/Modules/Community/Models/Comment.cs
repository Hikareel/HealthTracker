using HealthTracker.Server.Core.Models;
using HealthTracker.Server.Modules.Health.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Server.Modules.Community.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
        public int PostId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }
        [ForeignKey("ParentCommentId")]
        public Comment ParentComment { get; set; }
        public int? ParentCommentId { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "Must be 1000 characters or less!")]
        public string Content { get; set; }
        public DateTime? DateOfCreate { get; set; }
    }
}
