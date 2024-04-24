using System.ComponentModel.DataAnnotations;

namespace HealthTracker.Server.Modules.Community.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        [Required]
        [MaxLength(2500, ErrorMessage = "Must be 2500 characters or less!")]
        public string Content { get; set; }
        public DateTime? DateOfCreate { get; set; }
    }
}
