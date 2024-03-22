using HealthTracker.Server.Modules.Community.Models;

namespace HealthTracker.Server.Modules.Community.DTOs
{
    public class ChatMessagesDTO
    {
        public int Id { get; set; }
        public int UserFrom { get; set; }
        public int UserTo { get; set; }
        public int CurrentLastMessageId { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();

    }
}
