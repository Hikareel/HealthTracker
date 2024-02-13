using HealthTracker.Server.Modules.User.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Server.Modules.Community.Models
{
    public class Posts
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime DateOfCreation { get; set; }

        [ForeignKey("UserId")]
        public virtual Users User { get; set; }
    }
}
