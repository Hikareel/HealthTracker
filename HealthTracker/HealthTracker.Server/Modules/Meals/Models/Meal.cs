using System.ComponentModel.DataAnnotations;

namespace HealthTracker.Server.Modules.Meals.Models
{
    public class Meal
    {
        public int Id { get; set; }
        [MaxLength(1000, ErrorMessage = "Must be 1000 characters or less!")]
        public string Description { get; set; }
        public int? Calories { get; set; }
        public int? Carbs { get; set; }
        public int? Fat { get; set; }
        public int? Protein { get; set; }

        public virtual ICollection<MealUser> MealUsers { get; } = new List<MealUser>();
    }
}
