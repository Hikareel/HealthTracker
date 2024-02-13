using System.ComponentModel.DataAnnotations;

namespace HealthTracker.Server.Modules.Meals.Models
{
    public class Meals
    {
        [Required]
        public int MealId { get; set; }
        public int Calories { get; set; }
        public int Carbs {  get; set; }
        public int Fat {  get; set; }
        public int Protein { get; set; }
        public virtual ICollection<Meal_User> Meal_Users { get; set; }
    }
}
