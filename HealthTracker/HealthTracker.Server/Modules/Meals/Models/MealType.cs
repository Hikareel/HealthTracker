using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthTracker.Server.Modules.Meals.Models
{
    public class MealType
    {
        [Key]
        public int MealTypeId { get; set; }
        public string Name { get; set; }
        public int Meal_UserId { get; set; }
        [ForeignKey("Meal_UserId")]
        public virtual Meal_User Meal_User { get; set; }
    }
}
