using HealthTracker.Server.Modules.Health.Models;
using HealthTracker.Server.Modules.User.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Server.Modules.Community.Models
{
    public class Comments
    {
        public int CommendId { get; set; }
        public int PostId { get; set; }
        public string Content {  get; set; }
        public DateTime DateOfCreation { get; set; }
        public int NestedCommendId { get; set; }
        public int UserId { get; set; }

        [ForeignKey("PostId")]
        public virtual Posts Post { get; set; }
        [ForeignKey("UserId")]
        public virtual Users User { get; set; }
        [ForeignKey("NestedCommendId")]
        public virtual Comments Comment { get; set; }
    }
}
