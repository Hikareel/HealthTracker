using HealthTracker.Server.Modules.User.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Server.Modules.Community.Models
{
    public class Friendships
    {
        public int FriendshipId { get; set; }
        public int UserId1 { get; set; }
        public int UserId2 { get; set; }
        public string Status { get; set; }
        public DateTime DateOfCreation { get; set; }

        [ForeignKey("UserId1")]
        public virtual Users User1 { get; set; }
        [ForeignKey("UserId2")]
        public virtual Users User2 { get; set; }
    }
}
