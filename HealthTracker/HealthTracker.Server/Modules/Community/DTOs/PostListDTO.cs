using HealthTracker.Server.Modules.Community.Models;
using System.ComponentModel.DataAnnotations;

namespace HealthTracker.Server.Modules.Community.DTOs
{
    public class PostListDTO
    {
        public int UserId { get; set; }
        public List<PostDTO> Posts { get; set; } = new List<PostDTO>();
    }
}
