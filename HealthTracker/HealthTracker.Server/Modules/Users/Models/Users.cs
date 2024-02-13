using System.ComponentModel.DataAnnotations;

namespace HealthTracker.Server.Modules.User.Models
{
    public class Users
    {
        [Required]
        public int UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfCreation {  get; set; }
    }
}
