namespace HealthTracker.Server.Core.Exceptions.PhysicalActivity
{
    /// <summary>
    /// Represents an error when a workout cannot be found.
    /// </summary>
    public class WorkoutNotFoundException : Exception
    {
        public WorkoutNotFoundException() 
            :base("Workout not found!") { }
        public WorkoutNotFoundException(string message) 
            : base($"Workout not found! {message}") { }
        public WorkoutNotFoundException(int id)
            : base($"Workout not found! Id: {id}") { }
    }
}
