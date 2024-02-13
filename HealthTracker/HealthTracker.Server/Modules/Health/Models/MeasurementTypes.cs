using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Server.Modules.Health.Models
{
    public class MeasurementTypes
    {
        [Key]
        public int MeasurementTypeId { get; set; }
        public string Name {  get; set; }

        [ForeignKey("MeasurementTypeId")]
        public virtual HealthMeasurements HealthMeasurement { get; set; }
    }
}
