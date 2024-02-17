using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HealthTracker.Server.Core.Models;

namespace HealthTracker.Server.Modules.Health.Models
{
    public class HealthMeasurement
    {
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }
        [ForeignKey("MeasurementTypeId")]
        public MeasurementType MeasurementType { get; set; }
        public int MeasurementTypeId { get; set; }
        public float Value { get; set; }
        public DateTime? MeasurementDate { get; set; }
    }
}
