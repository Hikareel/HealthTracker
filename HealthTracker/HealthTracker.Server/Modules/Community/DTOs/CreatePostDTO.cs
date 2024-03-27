namespace HealthTracker.Server.Modules.Community.DTOs
{
    public class CreatePostDTO
    {
        public int UserId { get; set; }
        public required string Content { get; set; }
    }
}
