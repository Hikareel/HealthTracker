using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Server.Modules.Health.Models
{
    public class MeasurementType
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Must be 50 characters or less!")]
        public string ValueUnit { get; set; }
        [Required]
        [MaxLength(255, ErrorMessage = "Must be 255 characters or less!")]
        public string Name { get; set; }

        public virtual ICollection<HealthMeasurement> HealthMeasurements { get; } = new List<HealthMeasurement>();
    }
}
