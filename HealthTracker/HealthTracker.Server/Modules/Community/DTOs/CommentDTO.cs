using HealthTracker.Server.Core.Models;
using HealthTracker.Server.Modules.Community.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthTracker.Server.Modules.Community.DTOs
{
    public class CommentDTO
    {
        public CommentDTO()
        {
            AmountOfChildComments = 0;
        }
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public int? ParentCommentId { get; set; }
        public int AmountOfChildComments { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "Must be 1000 characters or less!")]
        public string Content { get; set; }
        public DateTime DateOfCreate { get; set; }
    }
}
