using HealthTracker.Server.Core.Models;
using HealthTracker.Server.Modules.Community.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthTracker.Server.Modules.Community.DTOs
{
    public class CreateCommentDTO
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public required string Content { get; set; }
    }
}
