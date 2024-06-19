using System.ComponentModel.DataAnnotations;

namespace HealthTracker.Server.Modules.Community.DTOs
{
    public class CreatePostDTO
    {
        public int UserId { get; set; }
        [Required]
        [MaxLength(2500, ErrorMessage = "Must be 2500 characters or less!")]
        public string Content { get; set; }
    }
}
