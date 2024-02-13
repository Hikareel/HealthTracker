using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthTracker.Server.Modules.User.Models;

namespace HealthTracker.Server.Modules.Meals.Models
{
    public class Meal_User
    {
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users User { get; set; }

        [Required]
        public int MealId { get; set; }
        [ForeignKey("MealId")]
        public virtual Meals Meal { get; set; }

        public DateTime Date { get; set; }
        // Relacja do MealType
        public List<MealType> MealTypes { get; set; }

    }
}
