using HealthTracker.Server.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Server.Modules.Community.Models
{
    public class Post
    {
        public Post()
        {
            DateOfCreate = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [Required]
        [MaxLength(2500, ErrorMessage = "Must be 2500 characters or less!")]
        public string Content { get; set; }
        public DateTime? DateOfCreate { get; set; }

        public virtual ICollection<Comment> Comments { get; } = new List<Comment>();
    }
}
