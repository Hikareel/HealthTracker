using HealthTracker.Server.Core.Models;
using HealthTracker.Server.Modules.Community.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthTracker.Server.Modules.Community.DTOs
{
    public class CommentFromPostDTO
    {
        public int PostId { get; set; }
        public int PageNr { get; set; }
        public int PageSize { get; set; }
        public int CommentsCount {  get; set; }
        public List<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
    }
}
