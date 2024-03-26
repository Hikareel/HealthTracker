using HealthTracker.Server.Modules.Community.Models;

namespace HealthTracker.Server.Modules.Community.DTOs
{
    public class ChatMessagesDTO
    {
        public int UserFrom { get; set; }
        public int UserTo { get; set; }
        public List<MessageDTO> Messages { get; set; } = new List<MessageDTO>();

    }
}
