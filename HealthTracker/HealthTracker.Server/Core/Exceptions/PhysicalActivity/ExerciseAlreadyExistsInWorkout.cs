namespace HealthTracker.Server.Core.Exceptions.PhysicalActivity
{
    /// <summary>
    /// Represents an error when a workout contains exercise.
    /// </summary>
    public class ExerciseAlreadyExistsInWorkout : Exception
    {
        public ExerciseAlreadyExistsInWorkout() 
            :base("Exercise Type not found!") { }
        public ExerciseAlreadyExistsInWorkout(string message) 
            : base($"Exercise Type not found! {message}") { }
        public ExerciseAlreadyExistsInWorkout(int id)
            : base($"Exercise already exists in workout! Id: {id}") { }
        public ExerciseAlreadyExistsInWorkout(int workoutId, int exerciseId)
            : base($"Exercise already exists in workout! Workout: {workoutId}, Exercise: {exerciseId}") { }
    }
}
