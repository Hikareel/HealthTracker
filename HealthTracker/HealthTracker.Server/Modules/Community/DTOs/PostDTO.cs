namespace HealthTracker.Server.Modules.Community.DTOs
{
    public class PostDTO
    {
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime? DateOfCreate { get; set; }
    }
}
