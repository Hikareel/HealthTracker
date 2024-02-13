using HealthTracker.Server.Modules.User.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthTracker.Server.Modules.Health.Models
{
    public class HealthMeasurements
    {
        public int HealthMeasurementId { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users User { get; set; }
        [Required]
        public int MeasurementTypeId { get; set; }
        [ForeignKey("MeasurementTypeId")]
        public virtual MeasurementTypes MeasurementType { get; set; }

        public float Value { get; set; }
        public DateTime MeasurementDate { get; set; }
    }
}
