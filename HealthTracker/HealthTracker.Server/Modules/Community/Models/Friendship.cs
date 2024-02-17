using HealthTracker.Server.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Server.Modules.Community.Models
{
    public class Friendship
    {
        public int Id { get; set; }
        [ForeignKey("User1Id")]
        public User User1 { get; set; }
        public int User1Id { get; set; }
        [ForeignKey("User2Id")]
        public User User2 { get; set; }
        public int User2Id { get; set; }
        [ForeignKey("StatusId")]
        public Status Status { get; set; }
        public int StatusId { get; set; }
        public DateTime? DateOfStart { get; set; }
    }
}
