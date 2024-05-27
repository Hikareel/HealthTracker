using HealthTracker.Server.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Server.Modules.Community.Models
{
    public class LikeDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int PostId { get; set; }
    }
}
