using HealthTracker.Server.Modules.Community.Models;
using System.ComponentModel.DataAnnotations;

namespace HealthTracker.Server.Modules.Community.DTOs
{
    public class PostDTO
    {
        public PostDTO()
        {
            AmountOfComments = 0;
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        [Required]
        [MaxLength(2500, ErrorMessage = "Must be 2500 characters or less!")]
        public string Content { get; set; }
        public DateTime? DateOfCreate { get; set; }
        public int AmountOfComments { get; set; }
        //public int AmountOfLikes { get; set; }
        public virtual ICollection<LikeDTO> Likes { get; set; } //Do zastanowienia: Jeśli np będzie 100 lajków to serwer będzie musiał zwrócić tablicę 100 elementów. Ale można to przerobić tak, żeby cała obsługa działa się na serwerze. Użytkownik klika przycisk i 
    }   //Można jeszcze zrobić aby była to lista samych userId i nazwać ją wtedy LikesBy czy coś takiego.
}
