using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthTracker.Server.Core.Models;

namespace HealthTracker.Server.Modules.Meals.Models
{
    public class MealUser
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }
        [ForeignKey("MealId")]
        public Meal Meal { get; set; }
        public int MealId { get; set; }
        [ForeignKey("MealTypeId")]
        public MealType MealType { get; set; }
        public int MealTypeId { get; set; }
        public DateTime? Date { get; set; }
    }
}
