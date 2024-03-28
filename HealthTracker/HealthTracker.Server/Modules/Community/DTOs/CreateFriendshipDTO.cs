using HealthTracker.Server.Core.Models;
using HealthTracker.Server.Modules.Community.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Server.Modules.Community.DTOs
{
    public class CreateFriendshipDTO
    {
        public int User1Id { get; set; }
        public int User2Id { get; set; }
    }
}
