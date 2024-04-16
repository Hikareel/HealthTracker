using System;

namespace HealthTracker.Server.Core.DTOs
{
    public class SuccessLoginDto
    {
        public SuccessLoginDto()
        {
            LoginTime = DateTime.UtcNow;
        }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime LoginTime { get; set; }
        public string Token { get; set; }
        
    }
}
