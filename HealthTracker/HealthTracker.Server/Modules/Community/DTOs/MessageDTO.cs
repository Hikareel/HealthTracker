using HealthTracker.Server.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Server.Modules.Community.DTOs
{
    public class MessageDTO
    {
        public bool IsUserMessage { get; set; }
        public required string Text { get; set; }
    }
}
