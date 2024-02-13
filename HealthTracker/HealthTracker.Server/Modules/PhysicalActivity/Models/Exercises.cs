namespace HealthTracker.Server.Modules.PhysicalActivity.Models
{
    public class Exercises
    {
        public int ExerciseId { get; set; }
        public int WorkoutId{ get; set; }
        public string ExerciseType { get; set;}
        public int Series {  get; set; }
        public int Reps { get; set; }

    }
}
