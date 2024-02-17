using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthTracker.Server.Modules.Meals.Models
{
    public class MealType
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255, ErrorMessage = "Must be 255 characters or less!")]
        public string Name { get; set; }

        public virtual ICollection<MealUser> MealUsers { get; } = new List<MealUser>();
    }
}
