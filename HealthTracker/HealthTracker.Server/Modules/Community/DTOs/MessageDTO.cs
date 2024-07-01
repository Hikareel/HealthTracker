namespace HealthTracker.Server.Modules.Community.DTOs
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public int UserIdFrom { get; set; }
        public int UserIdTo { get; set; }
        public bool IsReaded { get; set; }
        public required string Text { get; set; }
    }
}
