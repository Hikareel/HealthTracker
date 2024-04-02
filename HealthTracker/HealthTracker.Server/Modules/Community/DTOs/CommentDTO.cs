using HealthTracker.Server.Core.Models;
using HealthTracker.Server.Modules.Community.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthTracker.Server.Modules.Community.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int? ParentCommentId { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "Must be 1000 characters or less!")]
        public string Content { get; set; }
    }
}
