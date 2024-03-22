using HealthTracker.Server.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Server.Modules.Community.DTOs
{
    public class SendMessageDTO
    {
        public int UserIdTo { get; set; }
        public int UserIdFrom { get; set; }
        public string Text { get; set; }
    }
}
